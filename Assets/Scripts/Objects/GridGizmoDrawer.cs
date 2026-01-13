using UnityEngine;


[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{   
    
    public Vector2Int min = new(0, -80); // 가로 칸 수, 세로 칸 수 -> 내려가는 게임이므로 y는 음수
    public Vector2Int max = new( 10, 0); // 가로 칸 수, 세로 칸 수 -> 가로는 10칸
    public Vector3 startPosition;

    public float cellWidth = 1f;
    public float cellHeight = 1f;

    public void Start()
    {
        startPosition = transform.position;

    }
    private Vector3 GridToWorld(Vector2Int position)
    {
        return transform.position + new Vector3(position.x * cellWidth, position.y * cellHeight, 0f);
    }

    private void OnDrawGizmos()
    {
        for (int y = min.y; y <= max.y; y++)
        for (int x = min.x; x <= max.x; x++)
        {
            var position = new Vector2Int(x, y);
            var center = GridToWorld(position);

            var size = new Vector3(cellWidth, cellHeight, 0.02f); // Z만 얇게
                Gizmos.color = Color.white;      
                Gizmos.DrawWireCube(center, size);
        }
    }
}



