using UnityEngine;

public interface IPlayerRegistry
{
    
}


public class PlayerInstaller : MonoBehaviour, IPlayerRegistry
{
    [SerializeField] private IPlayerView _playerView;
    private PlayerModel _playerModel;
    [SerializeField] private PlayerDataSO _defaultPlayerData;
    private IPlayerPresenter _playerPresenter;

    public IPlayerView PlayerView => _playerView;
    public PlayerModel PlayerModel => _playerModel; 
    public IPlayerPresenter PlayerPresenter => _playerPresenter;



    #region Unity Lifecycle
    public void Awake()
    {
        _playerView = GetComponent<IPlayerView>();
    }
    #endregion

    public void SwitchPlayer(PlayerFactory playerFactory, PlayerDataSO playerModelSO)
    {
        _playerModel = playerFactory.CreatePlayer(_playerView).model;
        _playerPresenter = playerFactory.CreatePlayer(_playerView).presenter;
        
        _playerView.InstallPlayerView(_playerPresenter, playerModelSO.animatorController);
    }


}