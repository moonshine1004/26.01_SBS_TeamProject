using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDrawer : MonoBehaviour
{
    private static TileDrawer _tileDrawer;
    public static TileDrawer Instance
    {
        get
        {
            if (_tileDrawer == null)
            {
                _tileDrawer = FindFirstObjectByType<TileDrawer>();
            }
            return _tileDrawer;
        }
    }
    
    public List<Vector2> tileGrid = new List<Vector2>();
    public Vector2Int min = new(0, -80); // 가로 칸 수, 세로 칸 수 -> 내려가는 게임이므로 y는 음수
    public Vector2Int max = new( 10, 0); // 가로 칸 수, 세로 칸 수 -> 가로는 10칸
    public Vector3 startPosition;

    public float cellWidth = 1f;
    public float cellHeight = 1f;
    public Vector3 weight = new Vector3(0f, -0.5f, 0f); // 타일의 중심을 맞추기 위한 보정값

    public void Start()
    {
        startPosition = transform.position; // 씬 상의 실제 위치
        CreatTiles();
        Draw();
    }

    private Vector3 GridToWorld(Vector2Int position)
    {
        return startPosition + new Vector3(position.x * cellWidth, position.y * cellHeight, 0f);
    }

    private void Draw()
    {
        int i =0;
        for (int y = max.y; y >= min.y; y--)
            for (int x = min.x; x <= max.x; x++)
            {
                var position = new Vector2Int(x, y); // 그리드 상의 논리적 위치
                var center = GridToWorld(position); // 타일들의 씬 상의 실제 위치
                
                if (CheckTile(position)) // 타일 있는 칸
                {
                    TilePooling.Instance.GetTiles(i++).transform.position = center + weight;
                }
                else // 빈칸
                {
                    
                }
            }
    }
    
    public void CreatTiles() // 논리적 위치 생성
    {
        int x = 5;
        int minX = min.x;
        int maxX = max.x;

        for (int y = 0; y > -50; y--)
        {
            tileGrid.Add(new Vector2Int(x, y));

            if (x <= minX)
            {
                x++; // 오른쪽으로만
            }
            else if (x >= maxX)
            {
                x--; // 왼쪽으로만
            }
            else
            {
                x += Random.Range(0, 2) == 0 ? -1 : 1; // 계단이 이어지도록 x좌표에 -1 또는 +1을 더함
            }
        }
    }
    public bool CheckTile(Vector2 position)
    {
        return tileGrid.Contains(position);
    }
}