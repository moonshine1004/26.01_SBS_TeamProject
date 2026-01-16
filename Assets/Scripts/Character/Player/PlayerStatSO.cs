using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Character/Player/test")]
public class PlayerModelSO : ScriptableObject
{
    public int maxHP = 10;
    public RuntimeAnimatorController animatorController;

}