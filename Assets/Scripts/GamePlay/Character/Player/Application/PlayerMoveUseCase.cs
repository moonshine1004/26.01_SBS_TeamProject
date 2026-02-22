using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;


public interface IPlayerMoveSequence
{
    PlayerMovePlan Execute(Vector3 targetPosition);
}
public interface IPlayerMoveUseCase
{
    PlayerMovePlan Execute();
}

public class StartGameUseCase : IPlayerMoveSequence
{
    PositionDateS0 targetPositionData;

    public PlayerMovePlan Execute(Vector3 targetPosition)
    {
        PlayerMovePlan playerPlan = new PlayerMovePlan(PlayerState.StartGame, TargetPosition: targetPositionData.position, MoveDuration: 2f);
        return playerPlan;
    }
}

public class PlayerMoveUseCase : IPlayerMoveUseCase
{    
    public PlayerMovePlan Execute()
    {
        PlayerMovePlan playerPlan = new PlayerMovePlan(
            PlayerState.Walk, 
            TargetPosition: new Vector3(ConstVariable.xDistance, ConstVariable.yDistance, 0), 
            MoveDuration: 2f);
        return playerPlan;
    }
}
