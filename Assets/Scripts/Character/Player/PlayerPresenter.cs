using System;
using UnityEngine;
using Game.Events;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public interface IPlayerPresenter
{
    void Onhit(int damage);
    void OnMoveRequest();
    void OnFlipRequest();
    void OnRestartGame();
}

public class PlayerPresenter : IPlayerPresenter, IEventBusAware
{
    #region Constructor
    private PlayerModel _playerModel;
    private IPlayerView _playerView;

    public PlayerPresenter(PlayerModel playerModel, IPlayerView playerView)
    {
        _playerModel = playerModel;
        _playerView = playerView;
    }
    #endregion

    #region References
    private IEventBus _eventBus;
    #endregion

    #region Events
    private IDisposable _moveSub;
    private IDisposable _flipSub;
    private IDisposable _updateTileScore;
    private IDisposable _restartGame;
    #endregion
    #region Fields
    private Vector2 _startPosition = new Vector2(2,0);
    private Vector2 _position = new Vector2(2, 0);
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
            _playerView.forDebug("Death");
            GameStageManager.Instance.GameOver();
        }
    }
    
    public void SetEventBus(IEventBus bus)
    {
        _eventBus = bus;
        _moveSub = _eventBus.Subscribe<OnMovePressed>(_ => OnMoveRequest());
        _flipSub = _eventBus.Subscribe<OnFlipPressed>(_ => OnFlipRequest());
        _restartGame = _eventBus.Subscribe<OnRestartGame>(_ => OnRestartGame());
    }

    
    #region IPlayerPresenter Interface Implementation
    public void OnMoveRequest()
    {
        if(_isLeft)
            _position += new Vector2(-1, -1);
        else
            _position += new Vector2(1, -1);
        _playerView.SetPosition();
        _eventBus.Publish(new OnUpdateTileScore());
        
        CheckTile(_position);

        TileDrawer.Instance.UpdateTile();
    }

    public void OnFlipRequest()
    {
        _isLeft = !_isLeft;
        _playerView.SetDiraction();
        OnMoveRequest();
    }
    public void OnRestartGame()
    {
        _position = _startPosition;
        _isLeft = true;
        _playerView.SetStartPosition();
    }
    #endregion
}
