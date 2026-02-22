using UnityEngine;

public sealed class GameService 
{
    public GameService(PlayerFeature playerFeature)
    {
        PlayerFeature = playerFeature;
    }

    public PlayerFeature PlayerFeature { get; }
    public StartGameOrchestrator GameStartUseCase { get; private set; }

    
    
}

public sealed class PlayerFeature
{
    public PlayerFeature(IPlayerMoveSequence startGameUseCase, IPlayerMoveUseCase moveUseCase)
    {
        StartGameUseCase = startGameUseCase;
        MoveUseCase = moveUseCase;
    }
    
    public IPlayerMoveSequence StartGameUseCase { get; }
    public IPlayerMoveUseCase MoveUseCase { get; }
    public IPlayerMoveUseCase FlipUseCase { get; }
    public IPlayerMoveUseCase GoLobbyUseCase { get; }
    public IPlayerMoveUseCase ClearGameUseCase { get; }
    public IPlayerMoveUseCase RestartGameUseCase { get; }
    public IPlayerMoveUseCase DeathUseCase { get; }

}