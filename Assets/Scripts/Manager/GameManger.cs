using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instance;
    public GameManager Instance
    {
        get
        {
            if(_instance == null) _instance = new();
            return _instance;
        }
    }

    [SerializeField] private List<StageDataSO> _stageDatas = new List<StageDataSO>();
    private StageDataSO _currentStageDate;
    public StageDataSO CurrentStageData
    {
        get => _currentStageDate;
    }

    public void SwitchStage(int stageID)
    {
        foreach (StageDataSO stageData in _stageDatas)
        {
            if(stageData.stageID == stageID)
            {
                _currentStageDate = stageData;
            }
        }
    }

    public void OnStartButtonClick()
    {
        Debug.Log("Start Button Clicked");
    }

}