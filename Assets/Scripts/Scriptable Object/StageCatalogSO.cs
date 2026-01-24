using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stage/StageCatalogSO")]
public class StageCatalogSO : ScriptableObject
{
    public List<StageDataSO> stageDatas;
    public StageDataSO GetStageDataByID(int id)
    {
        foreach (var stageData in stageDatas)
        {
            if (stageData.id == id)
            {
                return stageData;
            }
        }
        return null;
    }
} 