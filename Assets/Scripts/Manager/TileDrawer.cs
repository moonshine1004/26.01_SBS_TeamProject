using System.Collections.Generic;
using Unity.VisualScripting;
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
    
    #region Fields
    [SerializeField] private List<Vector2> _tileGrid = new List<Vector2>();
    public Vector2Int min = new(0, -80); // 가로 칸 수, 세로 칸 수 -> 내려가는 게임이므로 y는 음수
    public Vector2Int max = new( 10, 0); // 가로 칸 수, 세로 칸 수 -> 가로는 10칸
    public Vector3 startPosition;
    private float cellWidth = ConstVariable.xDistance;
    private float cellHeight = ConstVariable.yDistance;
    public Vector3 weight = new Vector3(0f, -0.5f, 0f); // 타일의 중심을 맞추기 위한 보정값
    private int _tilePoolIndex = 0;
    private int _tilePoolWeight = 0;
    private int _lastXPos;
    private int _lastYPos;
    #endregion

    #region Unity Lifecycle
    
    #endregion

    public void OnStart()
    {
        startPosition = transform.position; // 씬 상의 실제 위치
        CreatTiles();
        DrawTiles();
    }
    public void OnRestart()
    {
        _tileGrid.Clear();
        min = new(0, -80);
        max = new(10, 0);
        _tilePoolIndex = 0;
        CreatTiles();
        DrawTiles();
    }
    public void CreatTiles() // 논리적 위치 생성
    {
        int x = 2; // 시작 칸
        for (int y = 0; y > -TilePooling.Instance.PoolAmount; y--)
        {
            _tileGrid.Add(new Vector2Int(x, y));

            if (x <= min.x)
            {
                x++; // 오른쪽으로만
            }
            else if (x >= max.x)
            {
                x--; // 왼쪽으로만
            }
            else
            {
                x += Random.Range(0, 2) == 0 ? -1 : 1; // 계단이 이어지도록 x좌표에 -1 또는 +1을 더함
            }
        }
        _lastXPos = x;
        _lastYPos = -TilePooling.Instance.PoolAmount;

    }
    private void DrawTiles()
    {
        for (int y = max.y; y >= min.y; y--)
            for (int x = min.x; x <= max.x; x++)
            {
                var position = new Vector2Int(x, y); // 그리드 상의 논리적 위치
                var center = GridToWorld(position); // 타일들의 씬 상의 실제 위치
                
                if (CheckTile(position)) // 타일 있는 칸
                {
                    TilePooling.Instance.GetTiles(_tilePoolIndex++).transform.position = center + weight;
                }
            }
    }
    private Vector3 GridToWorld(Vector2Int position)
    {
        return startPosition + new Vector3(position.x * cellWidth, position.y * cellHeight, 0f);
    }
    public bool CheckTile(Vector2 position)
    {
        return _tileGrid.Contains(position);
    }
    public void UpdateTile()
    {
        _tilePoolWeight++;
        if(_tilePoolWeight <7) return;
        
        if(_tilePoolIndex == TilePooling.Instance.PoolAmount)
        {
            _tilePoolIndex = 0;
        }
        TilePooling.Instance.ReturnTiles(_tilePoolIndex);
        _tileGrid.RemoveAt(0);

        _tileGrid.Add(new Vector2Int(_lastXPos ,_lastYPos));
        var center = GridToWorld(new Vector2Int(_lastXPos ,_lastYPos--));
        TilePooling.Instance.GetTiles(_tilePoolIndex++).transform.position = center + weight;
        if (_lastXPos <= min.x)
        {
            _lastXPos++; // 오른쪽으로만
        }
        else if (_lastXPos >= max.x)
        {
            _lastXPos--; // 왼쪽으로만
        }
        else
        {
            _lastXPos += Random.Range(0, 2) == 0 ? -1 : 1; // 계단이 이어지도록 x좌표에 -1 또는 +1을 더함
        }
    }

    private void Clear()
    {
        
    }
}