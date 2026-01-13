using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerView
{
    void InitPlayerView(IPlayerPresenter playerPresenter);
    void forDebug(string msg);
}

public class PlayerView : MonoBehaviour, IPlayerView
{
    private IPlayerPresenter _playerPresenter;
    
    #region Components
    private PlayerInput _playerInput;
    private InputAction _touchedPosition;
    #endregion
    #region Fields
    private int _xMove = 1;
    private int _yMove = 1;
    #endregion


    public void InitPlayerView(IPlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchedPosition = _playerInput.actions.FindAction("Touch/GetPosition", true);
    }


    // public void Move(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         Vector2 touchPosition = _touchedPosition.ReadValue<Vector2>();
            
    //         if(touchPosition.x < Screen.width * 0.5f)
    //         {
    //             this.gameObject.transform.position += new Vector3(-_xMove, -_yMove, 0);
    //             _playerPresenter.OnMove(true);
    //         } 
    //         else if(touchPosition.x > Screen.width * 0.5f)
    //         {
    //             this.gameObject.transform.position += new Vector3(_xMove, -_yMove, 0);
    //             _playerPresenter.OnMove(false);
    //         }
    //     }
    // }

    public void forDebug(string msg)
    {
        Debug.Log(msg);
    }
}
