using UnityEngine;


public interface IPlayerPresenter
{
    void Onhit(int damage);
    void OnMove(bool isLeft);
    void OnMoveInput();
    void OnFlipInput();
    int UpdateHP();
    int GetMaxHP();
}

public class PlayerPresenter : IPlayerPresenter
{
    private PlayerModel _playerModel;
    private IPlayerView _playerView;

    public PlayerPresenter(PlayerModel playerModel, IPlayerView playerView)
    {
        _playerModel = playerModel;
        _playerView = playerView;

        _uiPresenter.OnMoveInput += OnMoveInput;
        _uiPresenter.OnFlipInput += OnFlipInput;

        RuntimeManager.Instance.SetPlayerPresenter(this);
    }

    #region References
    [SerializeField] private IUIPresenter _uiPresenter;
    #endregion

    #region Events

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

    public void OnMove(bool isLeft)
    {
        if(isLeft)
            _position += new Vector2(-1, -1);
        else
            _position += new Vector2(1, -1);

        CheckTile(_position);
    }

    public void OnMoveInput()
    {
        if(_isLeft)
            _position += new Vector2(-1, -1);
        else
            _position += new Vector2(1, -1);

        CheckTile(_position);
    }

    public void OnFlipInput()
    {
        _isLeft = !_isLeft;
    }
}
