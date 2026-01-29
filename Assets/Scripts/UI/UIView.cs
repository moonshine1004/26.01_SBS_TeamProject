using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.InputSystem;
using Game.Events;
using System.Threading.Tasks;
using Unity.VisualScripting;

public interface IUIView
{
    void InitUIView(IUIPresenter uiPresenter);
    void TogglePopUp(SceneState sceneState);
    void ClearPopUp();
    void OnStartGame();
    void PopUpCanvas(CanvasType canvasType);
}

public enum CanvasType
{
    PausePopUp,
    SettingsPopUp
}

public class UIView : MonoBehaviour, IUIView
{
    private IUIPresenter _uiPresenter;
    public void InitUIView(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }
    
    #region References
    #endregion
    #region Feilds
    [SerializeField] private Canvas[] _allCanvas = new Canvas[7];
    [SerializeField] private Canvas _inGameUI;
    [SerializeField] private Canvas _gameOverPopUp;
    [SerializeField] private Canvas _gameClearPopUp;
    [SerializeField] private Canvas _lobby;
    [SerializeField] private Canvas _characterSelect;
    [SerializeField] private Canvas _mapSelect;
    [SerializeField] private Canvas _pausePopUp;
    [SerializeField] private Canvas _settingPopUp;
    [SerializeField] private GameObject _tutorials;
    [SerializeField] private GameObject _testMoveButton;
    [SerializeField] private GameObject _testFlipButton;
    #endregion

    #region Unity Lifecycle Methods
    private void Awake()
    {
        _allCanvas[0] = _inGameUI;
        _allCanvas[1] = _gameOverPopUp;
        _allCanvas[2] = _gameClearPopUp;
        _allCanvas[3] = _lobby;
        _allCanvas[4] = _characterSelect;
        _allCanvas[5] = _mapSelect;
    }
    #endregion
    #region Domain Methods
    private void ClearAllCanvas()
    {
        for(int i =0; i < _allCanvas.Length; i++)
        {
            _allCanvas[i].gameObject.SetActive(false);
        }
    }
    #endregion
    public void OnReturnLobby()
    {
        ClearAllCanvas();
        _uiPresenter.OnReturnLobbyRequest();
        _lobby.gameObject.SetActive(true);
    }
    public void PopUpSelectCharacter()
    {
        ClearAllCanvas();
        _characterSelect.gameObject.SetActive(true);
    }
    public void PopUpSelectMap()
    {
        ClearAllCanvas();
        _mapSelect.gameObject.SetActive(true);
    }
    public void PopUpCanvas(CanvasType canvasType)
    {
        switch(canvasType)
        {
            case CanvasType.PausePopUp:
                {
                    if (_pausePopUp.gameObject.activeSelf)
                    {
                        _pausePopUp.gameObject.SetActive(false);
                        break;
                    }
                    else
                    _pausePopUp.gameObject.SetActive(true);
                    break;
                }
            case CanvasType.SettingsPopUp:
                {
                    if (_settingPopUp.gameObject.activeSelf)
                    {
                        _settingPopUp.gameObject.SetActive(false);
                        break;
                    }
                    else
                    _settingPopUp.gameObject.SetActive(true);
                    break;
                }
            default:
                break;
        }
        
    }

    public async void WaitSecond(float seconds)
    {
        await Task.Delay((int)seconds * 1000); 
        _tutorials.SetActive(false);
    }

    #region Methods for Test
    public void MoveTest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _uiPresenter.OnMoveRequest();
            _testMoveButton.GetComponent<ButtonView>().OnButtonPressed();
            _testMoveButton.GetComponent<UIButtonSound>().PlaySound();
        }
            
    }
    public void FlipTest(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _uiPresenter.OnFlipRequest();
            _testFlipButton.GetComponent<ButtonView>().OnButtonPressed();
            _testFlipButton.GetComponent<UIButtonSound>().PlaySound();
        }
    }
    #endregion

    #region IUIView Interface Implementation
    public void TogglePopUp(SceneState sceneState)
    {
        switch(sceneState)
        {
            case SceneState.GameOver:
                {
                    _gameOverPopUp.gameObject.SetActive(true);
                    break;
                }
            case SceneState.GameClear:
                {
                    ClearAllCanvas();
                    _gameClearPopUp.gameObject.SetActive(true);
                    _inGameUI.gameObject.SetActive(true);
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
    public void ClearPopUp()
    {
        ClearAllCanvas();
        _inGameUI.gameObject.SetActive(true);
    }

    public void OnStartGame()
    {
        ClearAllCanvas();
        _tutorials.SetActive(true);
        _inGameUI.gameObject.SetActive(true);
        WaitSecond(ConstVariable.startAnimationTime);
    }
    #endregion
}