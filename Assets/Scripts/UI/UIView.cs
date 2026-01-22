using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.InputSystem;
using Game.Events;

public interface IUIView
{
    void InitUIView(IUIPresenter uiPresenter);
    void forDebug(string msg);
    void TogglePopUp(SceneState sceneState);
    void ClearPopUp();
    void UpdateScore();
    void UpdateTimer();

}

public class UIView : MonoBehaviour, IUIView
{
    private IUIPresenter _uiPresenter;
    public event Action TestEvent;
    public void InitUIView(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }
    
    #region References
    private IPlayerPresenter _playerPresenter;
    #endregion
    #region Feilds
    [SerializeField] private Canvas _gameOverPopUp;
    [SerializeField] private Text _tileScore;
    [SerializeField] private Text _currentScore;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _timer;
    #endregion

    #region Unity Lifecycle Methods
    #endregion

    


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
                {
                    _highScore.text = $"High Score: {ScoreManager.Instance.HighScore}";
                    _currentScore.text = $"Your Score: {ScoreManager.Instance.CurrentScore}";
                    _gameOverPopUp.gameObject.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }
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
    public void UpdateScore()
    {
        _tileScore.text = $"{ScoreManager.Instance.TileScore}";
    }
    public void ClearPopUp()
    {
        _gameOverPopUp.gameObject.SetActive(false);
    }
    public void UpdateTimer()
    {
        _timer.text = $"{GameStageManager.Instance.RemainingTime}";
    }
    #endregion
}