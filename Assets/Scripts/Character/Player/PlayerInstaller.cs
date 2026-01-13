using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private IPlayerView _playerView;
    [SerializeField] private PlayerStat _playerStat;
    private PlayerModel _playerModel;
    private IPlayerPresenter _playerPresenter;

    public void Awake()
    {
        _playerView = GetComponent<IPlayerView>();
        var _playerModel = InitPlayerModel(_playerStat);
        _playerPresenter = new PlayerPresenter(_playerModel, _playerView); 
        _playerView.InitPlayerView(_playerPresenter);
    }

    private PlayerModel InitPlayerModel(PlayerStat playerStat)
    {
        return new PlayerModel(playerStat.maxHP);
    }
}