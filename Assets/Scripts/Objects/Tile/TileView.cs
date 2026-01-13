using UnityEngine;

public interface ITileView
{
    GameObject obj { get; }
    void InitTileView(ITilePresenter tilePresenter);
}

public class TileView : MonoBehaviour, ITileView
{
    private ITilePresenter _tilePresenter;

    public GameObject obj => this.gameObject;

    public void InitTileView(ITilePresenter tilePresenter)
    {
        _tilePresenter = tilePresenter;
    }
}