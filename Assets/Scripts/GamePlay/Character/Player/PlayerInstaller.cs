using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private IPlayerView _playerView;
    [SerializeField] private PlayerDataSO _defaultPlayerData;
    private PlayerModel _playerModel;
    private IPlayerPresenter _playerPresenter;

    #region Unity Lifecycle
    public void Awake()
    {
        _playerView = GetComponent<IPlayerView>();
        _playerPresenter = new PlayerPresenter(_playerModel, _playerView); 
    }
    #endregion

    public void SwitchPlayer(PlayerDataSO playerModelSO)
    {
        _playerModel = InitPlayerModel(playerModelSO);
        _playerView.InstallPlayerView(_playerPresenter, playerModelSO.animatorController);
    }
    private PlayerModel InitPlayerModel(PlayerDataSO playerStat)
    {
        return new PlayerModel();
    }

}