using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerDataCatalogSO")]
public class PlayerDataCatalogSO : ScriptableObject
{
    public List<PlayerDataSO> playerDatas;
    public PlayerDataSO GetPlayerDataByID(int id)
    {
        foreach (var playerData in playerDatas)
        {
            if (playerData.id == id)
            {
                return playerData;
            }
        }
        return null;
    }
}   