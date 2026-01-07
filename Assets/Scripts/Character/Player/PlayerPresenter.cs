using UnityEngine;


public interface IPlayerPresenter
{
    void Onhit(int damage);
    int UpdateHP();
    int GetMaxHP();
}

public class PlayerPresenter : IPlayerPresenter
{
    private PlayerModel _playerModel;
    private IPlayerView _playerView;
    
    private static PlayerPresenter _instance;
    public static PlayerPresenter Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if(_instance == null)
                _instance = value;
            else
                Debug.LogWarning("이미 객체 존재");
        }
    }
    
    public PlayerPresenter(PlayerModel playerModel, IPlayerView playerView)
    {
        _playerModel = playerModel;
        _playerView = playerView;
    }

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
}
