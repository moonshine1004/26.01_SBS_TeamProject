using UnityEngine;

[CreateAssetMenu(
    menuName = "ScriptableObjects/Player/PlayerStat",
    fileName = "PlayerStat"
)]
public class PlayerStat : ScriptableObject
{
    public readonly PlayerType playerType;
    public readonly int _maxHP;
}