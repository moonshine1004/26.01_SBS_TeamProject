using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Character/Player/test")]
public class PlayerModelSO : ScriptableObject
{
    public int id;
    
    public RuntimeAnimatorController animatorController;

}