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

    [SerializeField] private List<StageDataSO> _stageDatas = new List<StageDataSO>();
    [SerializeField] private List<PlayerModelSO> _playerDatas = new List<PlayerModelSO>();

    [SerializeField] private PlayerInstaller _playerInstaller;

    #region Unity Lifecycle Methods
    private void Start()
    {
        
    }
    #endregion

    public void SwitchStage(int stageID)
    {
        foreach (StageDataSO stageData in _stageDatas)
        {
            if(stageData.stageID == stageID)
            {
                GameStageManager.Instance.UpdateSceneData(stageData);
                return;
            }
        }
    }
    public void SwitchPlayer(int playerID)
    {
        foreach (PlayerModelSO playerData in _playerDatas)
        {
            if(playerData.id == playerID)
            {
                _playerInstaller.SwitchPlayer(playerData);
                return;
            }
        }
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

}