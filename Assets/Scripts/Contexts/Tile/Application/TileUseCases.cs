using UnityEngine;

public interface ICheckTileUseCase
{
    void Execute();
}

public class CheckTileUseCase
{
    private readonly Vector2 _position;
    private readonly TileDrawer _tileDrawer;
    
    public CheckTileUseCase(PlayerPresenter player, TileDrawer tileDrawer)
    {
        _position = player.Position;
        _tileDrawer = tileDrawer;
    }

    public bool Execute()
    {
        return _tileDrawer.TileGrid.Contains(_position);
    }
}