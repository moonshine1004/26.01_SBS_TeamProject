using System.Collections.Generic;
using UnityEngine;

public class TilePooling : MonoBehaviour
{
    private static TilePooling _instance;
    public static TilePooling Instance
    {
        get{
            if(_instance == null)
            {
                _instance = FindFirstObjectByType<TilePooling>();
            }
            return _instance;
        }
    }
    
    [SerializeField] private GameObject _tilePrefab;
    private List<GameObject> _pooledTile = new List<GameObject>();

    private int poolAmount = 20;
    public int PoolAmount
    {
        get => poolAmount;
    }

    public void Awake()
    {
        CreatTiles();
    }

    public void CreatTiles()
    {
        for(int i = 0; i < poolAmount; i++)
        {
            var tile = InstantiateTiles();
            _pooledTile.Add(tile.obj);
            ReleaseTiles(tile.obj);
        }
    }

    private ITileView InstantiateTiles()
    {
        var tile = Instantiate(_tilePrefab);
        tile.TryGetComponent<ITileView>(out var tileView);
        return tileView;
    }
    private void ReleaseTiles(GameObject tile)
    {
        tile.SetActive(false);
    }
    public GameObject GetTiles(int num)
    {
        _pooledTile[num].SetActive(true);
        return _pooledTile[num];
    }
    public void ReturnTiles(int num)
    {
        _pooledTile[num].SetActive(false);
    }


}