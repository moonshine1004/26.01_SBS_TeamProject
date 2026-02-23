using UnityEngine;

public sealed class GameService 
{
    public GameService(PlayerFeature playerFeature)
    {
        PlayerFeature = playerFeature;
    }
    public GameService()
    {
        
    }

    public PlayerFeature PlayerFeature { get; }
    public StartGameOrchestrator GameStartUseCase { get; private set; }

    
    
}

public sealed class PlayerFeature
{
    public PlayerFeature(PlayerFactory playerFactory, IPlayerMoveSequence startGameUseCase, IPlayerMoveUseCase moveUseCase, SwitchPlayerUseCase switchPlayerUseCase)
    {
        PlayerFactory = playerFactory;
        StartGameUseCase = startGameUseCase;
        MoveUseCase = moveUseCase;
        SwitchPlayerUseCase = switchPlayerUseCase;
    }
    public SwitchPlayerUseCase SwitchPlayerUseCase { get; private set; }
    public PlayerFactory PlayerFactory { get; }
    public IPlayerMoveSequence StartGameUseCase { get; }
    public IPlayerMoveUseCase MoveUseCase { get; }
    public IPlayerMoveUseCase FlipUseCase { get; }
    public IPlayerMoveUseCase GoLobbyUseCase { get; }
    public IPlayerMoveUseCase ClearGameUseCase { get; }
    public IPlayerMoveUseCase RestartGameUseCase { get; }
    public IPlayerMoveUseCase DeathUseCase { get; }

}
