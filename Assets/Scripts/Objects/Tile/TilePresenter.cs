public interface ITilePresenter
{
    
}

public class TilePresenter
{
    private TileModel _tileModel;
    private TileView _tileView;

    public TileType GetTileType()
    {
        return _tileModel.TileType;
    }
}