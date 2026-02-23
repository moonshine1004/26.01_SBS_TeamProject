using UnityEngine;

public sealed class GameBootStrapper : MonoBehaviour
{
    
    
    public static GameService Services { get; private set; }
    
    [SerializeField] private CheckPointDataCatalogSO _positionDatas; 
    [SerializeField] private PlayerDataCatalogSO _playerDatas;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        

        // Infra 생성
        var playerFactory = new PlayerFactory();

        // UseCase 생성
        IPlayerMoveSequence startGameUseCase = new StartGameUseCase(_positionDatas);
        IPlayerMoveUseCase moveUseCase = new PlayerMoveUseCase();
        SwitchPlayerUseCase switchPlayerUseCase = new SwitchPlayerUseCase();
        //switchPlayerUseCase.Execute();

        // Feature 생성
        PlayerFeature playerFeature = new PlayerFeature(playerFactory, startGameUseCase, moveUseCase, switchPlayerUseCase);

        // Service 생성
        Services = new GameService(playerFeature);
    }
}
