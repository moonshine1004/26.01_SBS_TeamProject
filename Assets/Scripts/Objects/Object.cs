using System.Collections.Generic;
using UnityEngine;

public class Tiles
{
    public static Dictionary<Vector2, TileModel> tiles = new Dictionary<Vector2, TileModel>();

    public Dictionary<Vector2, TileModel> CreatTiles()
    {
        int i = 5;
        for(int j =0; j>-50; j--)
        {
            TileModel tile = new TileModel();
            tiles.Add(new Vector2(i, j), tile);
            i += Random.Range(0, 2) == 0 ? -1 : 1; // 계단이 이어지도록 x좌표에 -1 또는 +1을 더함
        }
        
        return tiles;
    }

    public bool CheckTile(Vector2 position)
    {
        return tiles.ContainsKey(position);
    }
}



