using UnityEngine;

public class TileInstaller :  MonoBehaviour
{
    private TileModel _tileModel;
    [SerializeField] private ITileView _tileView;
    private ITilePresenter _tilePresenter;

    public void Awake()
    {
        _tileView = GetComponent<ITileView>();
        _tileModel = new TileModel();
        _tilePresenter = new TilePresenter(_tileModel, _tileView);
        _tileView.InitTileView(_tilePresenter);
    }
}