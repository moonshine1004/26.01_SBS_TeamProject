using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.InputSystem;

public interface IUIView
{
    void InitUIView(IUIPresenter uiPresenter);
    void forDebug(string msg);

}

public class UIView : MonoBehaviour, IUIView
{
    private IUIPresenter _uiPresenter;
    
    public void InitUIView(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }
    
    #region References
    private IPlayerPresenter _playerPresenter;
    #endregion
    
    
    public void OnFlipButtonInput()
    {
        _uiPresenter.OnFlip();      
    }
    public void OnMoveButtonInput()
    {
        _uiPresenter.OnMove();
    }
    public void OnRestartButtonInput()
    {
        
    }
    



    public void forDebug(string msg)
    {
        Debug.Log(msg);
    }


    #region Methods for Test
    public void MoveTest(InputAction.CallbackContext context)
    {
        if(context.performed)
            _uiPresenter.OnMove();
    }
    public void FlipTest(InputAction.CallbackContext context)
    {
        if(context.performed)
            _uiPresenter.OnFlip();
    }
    #endregion
}