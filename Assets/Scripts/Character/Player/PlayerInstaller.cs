using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private PlayerStat _playerStat;
    private PlayerModel _playerModel;
    private PlayerPresenter _playerPresenter;

    public void Awake()
    {
        var _playerModel = InitPlayerModel(_playerStat);
        PlayerPresenter.Instance = new PlayerPresenter(_playerModel, _playerView); 
        _playerView.InitPlayerView(_playerPresenter);
    }

    private PlayerModel InitPlayerModel(PlayerStat playerStat)
    {
        return new PlayerModel(playerStat._maxHP);
    }
}