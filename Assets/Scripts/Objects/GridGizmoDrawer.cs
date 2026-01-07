using UnityEngine;



[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{
    public LevelGrid grid;

    public Vector2Int min = new(-2, -2);
    public Vector2Int max = new( 8, 10);

    public float cellSize = 1f;

    private Vector3 GridToWorld(Vector2Int g)
    {
        // 2D(XY) 기준: Z는 0으로 고정
        return new Vector3(g.x * cellSize, g.y * cellSize, 0f);
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;

        for (int y = min.y; y <= max.y; y++)
        for (int x = min.x; x <= max.x; x++)
        {
            var p = new Vector2Int(x, y);
            var center = GridToWorld(p);

            // 2D니까 Z두께만 아주 얇게
            var size = new Vector3(cellSize, cellSize, 0.02f);

            if (grid.HasTile(p)) // 타일 있는 칸
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

