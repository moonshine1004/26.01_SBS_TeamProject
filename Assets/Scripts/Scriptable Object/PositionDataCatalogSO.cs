using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Core/PositionDataCatalogSO")]
public class PositionDataCatalogSO : ScriptableObject
{
    public List<PositionDateS0> positionDataS;
    public PositionDateS0 GetPositionDataByName(string positionName)
    {
        foreach (var posData in positionDataS)
        {
            if (posData.positionName == positionName)
            {
                return posData;
            }
        }
        return null;
    }
}   