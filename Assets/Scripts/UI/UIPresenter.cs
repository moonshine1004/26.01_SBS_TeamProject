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
        EventBus.Instance.Subscribe<OnStartGame>(_ => OnStartGameRequest());
        EventBus.Instance.Subscribe<OnGameClear>(_ => OnGameClear());
        EventBus.Instance.Subscribe<OnGameOver>(_ => OnGameOverRequest());
    }
    #endregion


    #region IUIPresenter Interface Implementation
    public void OnFlipRequest()
    {
        EventBus.Instance.Publish(new OnFlipButtonPressed());
    }
    public void OnMoveRequest()
    {
        EventBus.Instance.Publish(new OnMoveButtonPressed());
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

    public void OnReturnLobbyRequest()
    {
        EventBus.Instance.Publish(new OnReturnLobby());
    }
    #endregion




}