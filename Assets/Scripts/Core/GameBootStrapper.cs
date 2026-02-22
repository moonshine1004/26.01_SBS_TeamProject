using UnityEngine;

public sealed class GameBootStrapper : MonoBehaviour
{
    public static GameService Services { get; private set; }
    
    [SerializeField] private PositionDataCatalogSO _positionDataCatalog; 

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Infra 생성
        PositionDateS0 _lobbyPositionData = _positionDataCatalog.GetPositionDataByName("PlayerStartPosition");

        // UseCase 생성
        IPlayerMoveSequence startGameUseCase = new StartGameUseCase();
        IPlayerMoveUseCase moveUseCase = new PlayerMoveUseCase();

        // Feature 생성
        PlayerFeature playerFeature = new PlayerFeature(startGameUseCase, moveUseCase);

        // Service 생성
        Services = new GameService(playerFeature);
    }
}
