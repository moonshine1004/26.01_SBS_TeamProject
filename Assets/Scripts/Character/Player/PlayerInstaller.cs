using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private IPlayerView _playerView;
    [SerializeField] private PlayerModelSO _playerStat;
    private PlayerModel _playerModel;
    private IPlayerPresenter _playerPresenter;

    [SerializeField] private EventBus _eventBus;

    #region Unity Lifecycle
    public void Awake()
    {
        _playerView = GetComponent<IPlayerView>();
        _playerModel = InitPlayerModel(_playerStat);
        _playerPresenter = new PlayerPresenter(_playerModel, _playerView); 
        if (_playerPresenter is IEventBusAware busAware)
        {
            busAware.SetEventBus(_eventBus);
        }
        _playerView.InstallPlayerView(_playerPresenter, _playerStat.animatorController);
    }
    #endregion


    private PlayerModel InitPlayerModel(PlayerModelSO playerStat)
    {
        return new PlayerModel(playerStat.maxHP);
    }

}