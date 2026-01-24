using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Stage/Stage")]
public class StageDataSO : ScriptableObject
{
    public int id;
    public float time;
    public int endLine;   
}