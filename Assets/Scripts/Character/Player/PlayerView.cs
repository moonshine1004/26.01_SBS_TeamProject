using UnityEngine;

public interface IPlayerView
{
    
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerPresenter _playerPresenter;
    
    private Rigidbody2D _rb;
    private Animator _animator;

    public void InitPlayerView(PlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }

    private void OllisionEnter2D(Collision2D collision)
    {
 
    }
}
