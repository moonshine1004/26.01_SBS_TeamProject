using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public sealed class LevelGrid : MonoBehaviour
{
    // 타일이 있는 좌표만 저장
    public HashSet<Vector2Int> tiles = new();

    private void OnEnable()
    {
        // 프로토타입용: 코드로 타일 배치
        tiles.Clear();

        // 예시: 지그재그 계단
        tiles.Add(new Vector2Int(0, 0));
        tiles.Add(new Vector2Int(1, 1));
        tiles.Add(new Vector2Int(0, 2));
        tiles.Add(new Vector2Int(1, 3));
        tiles.Add(new Vector2Int(0, 4));
    }

    public bool HasTile(Vector2Int p)
    {
        return tiles.Contains(p);
    }
}