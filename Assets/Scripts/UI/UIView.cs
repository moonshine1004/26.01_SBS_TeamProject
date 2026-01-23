using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.InputSystem;
using Game.Events;
using System.Threading.Tasks;

public interface IUIView
{
    void InitUIView(IUIPresenter uiPresenter);
    void forDebug(string msg);
    void TogglePopUp(SceneState sceneState);
    void ClearPopUp();
    void UpdateScore();
    void UpdateTimer();
    void OnStartGame();

}

public class UIView : MonoBehaviour, IUIView
{
    private IUIPresenter _uiPresenter;
    public void InitUIView(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
        EventBus.Instance.Subscribe<OnStartGame>(_ => OnStartGame());
    }
    
    #region References
    private IPlayerPresenter _playerPresenter;
    #endregion
    #region Feilds
    [SerializeField] private Canvas[] _allCanvas = new Canvas[7];
    [SerializeField] private Canvas _inGameUI;
    [SerializeField] private Canvas _gameOverPopUp;
    [SerializeField] private Canvas _gameClearPopUp;
    [SerializeField] private Canvas _lobby;
     [SerializeField] private Canvas _characterSelect;
     [SerializeField] private Canvas _mapSelect;
    [SerializeField] private Text _tileScore;
    [SerializeField] private Text _currentScore;
    [SerializeField] private Text _highScore;
    [SerializeField] private Text _timer;
    [SerializeField] private GameObject _tutorials;
    #endregion

    #region Unity Lifecycle Methods
    private void Awake()
    {
        _allCanvas[1] = _inGameUI;
        _allCanvas[2] = _gameOverPopUp;
        _allCanvas[3] = _gameClearPopUp;
        _allCanvas[4] = _lobby;
        _allCanvas[5] = _characterSelect;
        _allCanvas[6] = _mapSelect;
    }
    #endregion

    private void SetActiveAllCanvasFalse()
    {
        for(int i =0; i < _allCanvas.Length; i++)
        {
            _allCanvas[i].gameObject.SetActive(false);
        }
    }
    public void OnReturnLobby()
    {
        SetActiveAllCanvasFalse();
        _lobby.gameObject.SetActive(true);
    }
    public void PopUpSelectCharacter()
    {
        SetActiveAllCanvasFalse();
        _characterSelect.gameObject.SetActive(true);
    }
    public void PopUpSelectMap()
    {
        SetActiveAllCanvasFalse();
        _mapSelect.gameObject.SetActive(true);
    }

    public async void WaitSecond(float seconds)
    {
        await Task.Delay((int)seconds * 1000); 
        _tutorials.SetActive(false);
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
                {
                    _highScore.text = $"{ScoreManager.Instance.HighScore}";
                    _currentScore.text = $"{ScoreManager.Instance.CurrentScore}";
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

    public void OnStartGame()
    {
        SetActiveAllCanvasFalse();
        _tutorials.SetActive(true);
        _inGameUI.gameObject.SetActive(true);
        WaitSecond(ConstVariable.startAnimationTime);
    }
    #endregion
}