using UnityEngine;
using UnityEngine.Rendering;



[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{
    public LevelGrid grid;
    private Tiles _tiles = new Tiles();

    
    public Vector2Int min = new(-2, -2);
    public Vector2Int max = new( 8, 10);
    public Vector3 startPosition;

    public float cellWidth = 1f;
    public float cellHeight = 0.5f;

    public void Awake()
    {
        _tiles.CreatTiles();
    }
    public void Update()
    {
        startPosition = transform.position;
    }
    private Vector3 GridToWorld(Vector2Int position)
    {
        return transform.position + new Vector3(position.x * cellWidth, position.y * cellHeight, 0f);
    }

    // private void OnDrawGizmos()
    // {
    //     if (grid == null) return;

    //     for (int y = min.y; y <= max.y; y++)
    //     for (int x = min.x; x <= max.x; x++)
    //     {
    //         var p = new Vector2Int(x, y);
    //         var center = startPosition + GridToWorld(p);

    //         var size = new Vector3(cellWidth, cellHeight, 0.02f); // Z만 얇게

    //         if (grid.HasTile(p)) // 타일 있는 칸
    //         {
    //             Gizmos.color = Color.green;      
    //             Gizmos.DrawCube(center, size);
    //         }
    //         else // 빈칸
    //         {
    //             Gizmos.color = Color.white;      
    //             Gizmos.DrawWireCube(center, size);
    //         }
    //     }
    // }

    private void OnDrawGizmos()
    {
        for (int y = min.y; y <= max.y; y++)
        for (int x = min.x; x <= max.x; x++)
        {
            var position = new Vector2Int(x, y);
            var center = GridToWorld(position);

            var size = new Vector3(cellWidth, cellHeight, 0.02f); // Z만 얇게

            if (_tiles.CheckTile(position)) // 타일 있는 칸
            {
                Gizmos.color = Color.green;      
                Gizmos.DrawCube(center, size);
            }
            else // 빈칸
            {
                Gizmos.color = Color.white;      
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}

