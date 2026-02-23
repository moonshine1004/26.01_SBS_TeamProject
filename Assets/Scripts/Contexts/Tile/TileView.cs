using UnityEngine;

public interface ITileView
{
    GameObject obj { get; }

}

public class TileView : MonoBehaviour, ITileView
{
    public GameObject obj => this.gameObject;
}