using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class GridGizmoDrawer : MonoBehaviour
{
    private Tiles _tiles = new Tiles();

    
    public Vector2Int min = new(0, -80);
    public Vector2Int max = new( 10, 0);
    public Vector3 startPosition;

    public float cellWidth = 1f;
    public float cellHeight = 1f;


    [ContextMenu("Creat Tiles")]
    public void CreatTiles()
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
                Gizmos.DrawCube(center, size + new Vector3(0, -cellHeight/2, 0));
            }
            else // 빈칸
            {
                Gizmos.color = Color.white;      
                Gizmos.DrawWireCube(center, size);
            }
        }
    }
}

