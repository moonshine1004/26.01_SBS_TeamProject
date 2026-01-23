using System;
using UnityEngine;
using Game.Events;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;


public interface IPlayerPresenter
{
    void Onhit(int damage);
    void OnMoveRequest();
    void OnFlipRequest();
    void OnStartGame();
    void OnRestartGame();
    void OnReturnLobby();
}

public class PlayerPresenter : IPlayerPresenter
{
    #region Constructor
    private PlayerModel _playerModel;
    private IPlayerView _playerView;

    public PlayerPresenter(PlayerModel playerModel, IPlayerView playerView)
    {
        _playerModel = playerModel;
        _playerView = playerView;

        _moveSub = EventBus.Instance.Subscribe<OnMovePressed>(_ => OnMoveRequest());
        _flipSub = EventBus.Instance.Subscribe<OnFlipPressed>(_ => OnFlipRequest());
        _startGame = EventBus.Instance.Subscribe<OnStartGame>(_ => OnStartGame());
        _restartGame = EventBus.Instance.Subscribe<OnRestartGame>(_ => OnRestartGame());
    }
    #endregion

    #region References

    #endregion

    #region Events
    private IDisposable _moveSub;
    private IDisposable _flipSub;
    private IDisposable _updateTileScore;
    private IDisposable _startGame;
    private IDisposable _restartGame;
    #endregion
    #region Fields
    private Vector2 _startPosition = new Vector2(2,0);
    private Vector2 _position = new Vector2(2, 0);
    private bool _canMove = false;
    private bool _isLeft = true;
    #endregion

    private TilePresenter _tilePresenter;
    
    public void Onhit(int damage)
    {
        _playerModel.HP -= damage;
    }
    public void CheckTile(Vector2 position)
    {
        if (!TileDrawer.Instance.CheckTile(position))
        {
            _playerView.SetDeath();
            GameStageManager.Instance.GameOver();
        }
    }

    public async void WaitSecond(float seconds)
    {
        await Task.Delay((int)seconds * 1000); 
        _canMove = true;
    }
    
    #region IPlayerPresenter Interface Implementation
    public void OnMoveRequest()
    {
        if(!_canMove) return;
        if(_isLeft)
            _position += new Vector2(-1, -1);
        else
            _position += new Vector2(1, -1);
        _playerView.SetPosition();
        EventBus.Instance.Publish(new OnUpdateTileScore());
        
        CheckTile(_position);

        TileDrawer.Instance.UpdateTile();
    }

    public void OnFlipRequest()
    {
        if(!_canMove) return;
        _isLeft = !_isLeft;
        _playerView.SetDiraction();
        OnMoveRequest();
    }
    public void OnStartGame()
    {
        _playerView.SetStartGame();
        WaitSecond(ConstVariable.startAnimationTime);
    }
    public void OnRestartGame()
    {
        _position = _startPosition;
        _isLeft = true;
        _playerView.SetRestartGame();
    }
    public void OnReturnLobby()
    {
        _canMove = false;
        _playerView.SetReturnLobby();
        _position = _startPosition;
        _isLeft = true;
    }
    
    #endregion
}
