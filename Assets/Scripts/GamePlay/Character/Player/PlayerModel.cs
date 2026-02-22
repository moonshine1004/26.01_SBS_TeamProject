using UnityEngine;

public class PlayerModel
{
    public PlayerModel()
    {
        
    }
    public PlayerMovementModel PlayerMoveState { get; private set; } = new();

}

public class PlayerMovementModel
{
    public bool CanMove { get; private set; } = false;
    public bool IsLeft { get; private set; } = true;
    public bool IsMoving { get; private set; } = false;

    public void SetState(bool canMove, bool isLeft, bool isMoving)
    {
        CanMove = canMove;
        IsLeft = isLeft;
        IsMoving = isMoving;
    }

    public void SetCanMove(bool canMove)
    {
        CanMove = canMove;
    }
    public void FlipIsLeft()
    {
        IsLeft =! IsLeft;
    }
    public void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }
}
