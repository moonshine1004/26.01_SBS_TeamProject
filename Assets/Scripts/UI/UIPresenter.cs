using System;
using Game.Events;

public interface IUIPresenter
{
    void OnMoveRequest();
    void OnFlipRequest();
    void OnStartGameRequest();
    void OnGameOverRequest();
    void OnGameClear();
    void OnRestartRequest();
    void OnReturnLobbyRequest();
    void OnUpdateScoreRequest();
    void OnUpdateTimerRequest();
}

public class UIPresenter : IUIPresenter
{
    #region Constructor
    private UIModel _uiModel;
    private IUIView _uiView;
    
    public UIPresenter(UIModel uiModel, IUIView uiView)
    {
        _uiModel = uiModel;
        _uiView = uiView;
        EventBus.Instance.Subscribe<OnUpdateTileScore>(_ => OnUpdateScoreRequest());
        EventBus.Instance.Subscribe<OnTimeChange>(_ => OnUpdateTimerRequest());
        EventBus.Instance.Subscribe<OnStartGame>(_ => OnStartGameRequest());
        EventBus.Instance.Subscribe<OnGameClear>(_ => OnGameClear());
        EventBus.Instance.Subscribe<OnGameOver>(_ => OnGameOverRequest());
    }
    #endregion


    #region IUIPresenter Interface Implementation
    public void OnFlipRequest()
    {
        EventBus.Instance.Publish(new OnFlipPressed());
    }
    public void OnMoveRequest()
    {
        EventBus.Instance.Publish(new OnMovePressed());
    }
    public void OnStartGameRequest()
    {
        _uiView.OnStartGame();
    }
    public void OnGameOverRequest()
    {
        _uiView.TogglePopUp(SceneState.GameOver);
    }
    public void OnGameClear()
    {
        _uiView.TogglePopUp(SceneState.GameClear);
    }
    public void OnRestartRequest()
    {
        EventBus.Instance.Publish(new OnRestartGame());
        _uiView.ClearPopUp();
    }
    public void OnUpdateScoreRequest()
    {
        _uiView.UpdateScore();
    }
    public void OnUpdateTimerRequest()
    {
        _uiView.UpdateTimer();
    }

    public void OnReturnLobbyRequest()
    {
        EventBus.Instance.Publish(new OnReturnLobby());
    }
    #endregion




}