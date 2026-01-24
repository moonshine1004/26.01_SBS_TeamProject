using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    public int id;
    public float bonusRate;
    public RuntimeAnimatorController animatorController;

}