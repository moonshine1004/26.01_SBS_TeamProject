using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
public class StageDataSO : ScriptableObject
{
    public int stageID;
    public float time;
    public int endLine;   
}