using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class BootStrap : MonoBehaviour
{
    [SerializeField] private TileDrawer _tileDrawer;
    private PlayerPresenter _playerPresenter;

    public static GameServices Services { get; private set; }
    private void Awake()
    {
        // Infra
        
        // Application
        var checkTileUseCase = new CheckTileUseCase(Vector2.zero, _tileDrawer);

        // Feature

        // View
        // _tileDrawer
        var tilePooling = new TilePooling();

        Services = new GameServices();
        Services.Register(checkTileUseCase);
    }
}