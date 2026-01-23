using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private IPlayerView _playerView;
    [SerializeField] private PlayerModelSO _playerStat;
    private PlayerModel _playerModel;
    private IPlayerPresenter _playerPresenter;

    #region Unity Lifecycle
    public void Awake()
    {
        _playerView = GetComponent<IPlayerView>();
        _playerModel = InitPlayerModel(_playerStat);
        _playerPresenter = new PlayerPresenter(_playerModel, _playerView); 
        _playerView.InstallPlayerView(_playerPresenter, _playerStat.animatorController);
    }
    #endregion

    public void SwitchPlayer(PlayerModelSO playerModelSO)
    {
        _playerView.InstallPlayerView(_playerPresenter, playerModelSO.animatorController);
        Debug.Log("Player Switched to ID: " + playerModelSO.id);
    }
    private PlayerModel InitPlayerModel(PlayerModelSO playerStat)
    {
        return new PlayerModel();
    }

}