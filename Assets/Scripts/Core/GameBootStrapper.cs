using UnityEngine;

public sealed class GameBootStrapper : MonoBehaviour
{
    
    
    public static GameService Services { get; private set; }
    
    [SerializeField] private CheckPointDataCatalogSO _positionDataCatalog; 

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Infra 생성
        

        // UseCase 생성
        IPlayerMoveSequence startGameUseCase = new StartGameUseCase(_positionDataCatalog);
        IPlayerMoveUseCase moveUseCase = new PlayerMoveUseCase();

        // Feature 생성
        PlayerFeature playerFeature = new PlayerFeature(startGameUseCase, moveUseCase);

        // Service 생성
        Services = new GameService(playerFeature);
    }
}
