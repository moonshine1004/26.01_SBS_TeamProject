using System;
using UnityEngine;
using Game.Events;
using UnityEngine.Tilemaps;


public interface IPlayerPresenter
{
    void Onhit(int damage);
    void OnMoveRequest();
    void OnFlipRequest();
    int UpdateHP();
    int GetMaxHP();
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
    #endregion
    #region Fields
    private Vector2 _position = new Vector2(5, 0);
    private bool _isLeft = true;
    #endregion

    private TilePresenter _tilePresenter;
    
    public void Onhit(int damage)
    {
        _playerModel.HP -= damage;
    }
    public int UpdateHP()
    {
        return _playerModel.HP;
    }
    public int GetMaxHP()
    {
        return _playerModel.MaxHP;
    }
    public void CheckTile(Vector2 position)
    {
        if (!TileDrawer.Instance.CheckTile(position))
        {
            Onhit(1);
            _playerView.forDebug("체력 -1");
        }
    }

    public void OnMoveRequest()
    {
        if(_isLeft)
            _position += new Vector2(-1, -1);
        else
            _position += new Vector2(1, -1);
        _playerView.SetPosition();
        CheckTile(_position);

        TileDrawer.Instance.UpdateTile();
    }

    public void OnFlipRequest()
    {
        _isLeft = !_isLeft;
        _playerView.SetDiraction();
    }

    public void SetEventBus(IEventBus bus)
    {
        _eventBus = bus;
        _moveSub = _eventBus.Subscribe<OnMovePressed>(_ => OnMoveRequest());
        _flipSub = _eventBus.Subscribe<OnFlipPressed>(_ => OnFlipRequest());
    }
}
