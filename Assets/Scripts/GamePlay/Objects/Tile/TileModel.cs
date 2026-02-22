
public enum TileType
{
    Walkable,
    Unwalkable,
    Damage1,
    Damage2
}

public class TileModel
{
    private TileType tileType;
    public TileType TileType
    {
        get => tileType;
    }
}