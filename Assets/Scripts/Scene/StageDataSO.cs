using UnityEngine;

public interface IStageData
{
    StageDataContext ChangeStrategy();
}

public struct StageDataContext
{
    public int stageID;
    public float time;
    public int endLine; 
}

[CreateAssetMenu(menuName = "ScriptableObjects/LevelData")]
public class StageDataSO : ScriptableObject, IStageData
{
    public int stageID;
    public float time;
    public int endLine;   
    public StageDataContext ChangeStrategy()
    {
        StageDataContext sceneDataContext = new StageDataContext();
        sceneDataContext.stageID = stageID;
        sceneDataContext.time = time;
        sceneDataContext.endLine = endLine;

        return sceneDataContext;
    }
}