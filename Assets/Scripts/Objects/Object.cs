using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Walkable,
    Unwalkable,
    Damage1,
    Damage2
}

public class Tile : MonoBehaviour
{
    [SerializeField] private Vector2Int gridPos;
    public TileType tileType;
    public Vector2Int GridPos => gridPos;
}

public class LevelGrig : MonoBehaviour
{
    private readonly Dictionary<Vector2Int, Tile> _tiles = new ();
}


[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{
    public Vector2Int min = new(-10, -10);
    public Vector2Int max = new( 10,  30);

    public float cellSize = 1f;
    public Vector3 origin = Vector3.zero;

    public Vector3 GridToWorld(Vector2Int g)
    {
        return origin + new Vector3(
            g.x * cellSize, 
            0f,
            g.y * cellSize
        );
    }

}