using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.InputSystem;
using UnityEditor.SearchService;

public interface IUIView
{
    void InitUIView(IUIPresenter uiPresenter);
    void forDebug(string msg);
    void TogglePopUp(SceneState sceneState);
    void ClearPopUp();

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
    #region Feilds
    [SerializeField] private Canvas _gameOverPopUp;
    #endregion
    
    
    public void OnFlipButtonInput()
    {
        _uiPresenter.OnFlipRequest();      
    }
    public void OnMoveButtonInput()
    {
        _uiPresenter.OnMoveRequest();
    }
    public void OnRestartButtonInput()
    {
        _uiPresenter.OnRestartRequest();
    }
    
    



    public void forDebug(string msg)
    {
        Debug.Log(msg);
    }


    #region Methods for Test
    public void MoveTest(InputAction.CallbackContext context)
    {
        if(context.performed)
            _uiPresenter.OnMoveRequest();
    }
    public void FlipTest(InputAction.CallbackContext context)
    {
        if(context.performed)
            _uiPresenter.OnFlipRequest();
    }
    #endregion

    #region IUIView Interface Implementation
    public void TogglePopUp(SceneState sceneState)
    {
        switch(sceneState)
        {
            case SceneState.GameOver:
                _gameOverPopUp.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void ClearPopUp()
    {
        _gameOverPopUp.gameObject.SetActive(false);
    }
    #endregion
}