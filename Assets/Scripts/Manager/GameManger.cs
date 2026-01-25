using System.Collections.Generic;
using UnityEngine;
using Game.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private PlayerInstaller _playerInstaller;
    [SerializeField] private PlayerCatalogSO _playerCatalogSO;
    [SerializeField] private StageCatalogSO _stageCatalogSO;
    [SerializeField] private PlayerDataSO _currentPlayerData;
    [SerializeField] private StageDataSO _currentStageData;
    public PlayerDataSO CurrentPlayerData => _currentPlayerData;
    public StageDataSO CurrentStageData => _currentStageData;

    #region Unity Lifecycle Methods
    private void Start()
    {
        SwitchStage(GamePrefsRepository.CurrentPlayMap);
        SwitchPlayer(GamePrefsRepository.CurrentPlayPlayer);
    }
    #endregion

    #region Test Methods
    [ContextMenu("Clear PlayerPrefs")]
    private void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    #endregion

    #region Public Methods
    public void SwitchStage(int stageID)
    {
        _currentStageData = _stageCatalogSO.GetStageDataByID(stageID);
        GamePrefsRepository.CurrentPlayMap = stageID;
        GameStageManager.Instance.UpdateSceneData(_currentStageData);
    }
    public void SwitchPlayer(int playerID)
    {
        _currentPlayerData = _playerCatalogSO.GetPlayerDataByID(playerID);
        GamePrefsRepository.CurrentPlayPlayer = playerID;
        _playerInstaller.SwitchPlayer(_currentPlayerData);
    }
    public void Switch(ButtonType buttonType, int id)
    {
        switch (buttonType)
        {
            case ButtonType.PlayerSelect:
                {
                    SwitchPlayer(id);
                }
                break;
            case ButtonType.StageSelect:
                {
                    SwitchStage(id);
                }
                break;
        }
    }

    public void OnStartButtonClick()
    {
        EventBus.Instance.Publish(new OnStartGame());
    }
    public void OnQuitButtonClick()
    {
        EventBus.Instance.Publish(new OnQuitGame());
    }
    #endregion

}