using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerCatalogSO")]
public class PlayerCatalogSO : ScriptableObject
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