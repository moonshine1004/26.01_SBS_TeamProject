using System.Collections;
using Unity.Cinemachine;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;


public interface IPlayerMoveSequence
{
    PlayerMovePlan Execute();
}
public interface IPlayerMoveUseCase
{
    PlayerMovePlan Execute();
}
public readonly struct PlayerMovePlan
{
    public PlayerState State { get; }
    public Vector3 StartPosition { get; }
    public Vector3 TargetPosition { get; } 
    public bool IsLeft { get; }
    public float MoveDuration { get; }
    public PlayerMovePlan(PlayerState State, Vector3 StartPosition = default, bool IsLeft = default, Vector3 TargetPosition = default, float MoveDuration = default)
    {
        this.State = State;
        this.StartPosition = StartPosition;
        this.TargetPosition = TargetPosition;
        this.IsLeft = IsLeft;
        this.MoveDuration = MoveDuration;
    }
}
public class StartGameUseCase : IPlayerMoveSequence
{
    public StartGameUseCase(ICheckPinterProvider checkPinterProvider)
    {
        _targetPosition = checkPinterProvider;
    }
    
    private ICheckPinterProvider _targetPosition;

    public PlayerMovePlan Execute()
    {
        PlayerMovePlan playerPlan = new PlayerMovePlan(PlayerState.StartGame, TargetPosition: _targetPosition.GetCheckPointData("test").position, MoveDuration: 2f);
        return playerPlan;
    }
}

public class PlayerMoveUseCase : IPlayerMoveUseCase
{    
    private PlayerModel _playerModel;
    
    public PlayerMovePlan Execute()
    {
        var targetPos = _playerModel.PlayerMoveState.IsLeft ? new Vector3(-ConstVariable.xDistance, ConstVariable.yDistance, 0) : new Vector3(ConstVariable.xDistance, ConstVariable.yDistance, 0);
        
        PlayerMovePlan playerPlan = new PlayerMovePlan(
            PlayerState.Walk, 
            IsLeft: _playerModel.PlayerMoveState.IsLeft,
            TargetPosition: targetPos, 
            MoveDuration: 2f);
        return playerPlan;
    }
}
