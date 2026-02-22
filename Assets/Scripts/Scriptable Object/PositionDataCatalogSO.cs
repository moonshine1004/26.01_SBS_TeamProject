using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CheckPoint")]
public class CheckPointDataCatalogSO : ScriptableObject, ICheckPinterProvider
{
    public List<CheckPointDateS0> positionDatas;
    public Dictionary<string, CheckPointDateS0> positionDataDictionary;

    public CheckPointDateS0 GetCheckPointData(string positionName)
    {
        foreach (var posData in positionDatas)
        {
            if (posData.positionName == positionName)
            {
                return posData;
            }
        }
        return null;
    }
}   

public interface ICheckPinterProvider
{
    CheckPointDateS0 GetCheckPointData(string positionName);
}