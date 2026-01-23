using System;
using Game.Events;

public interface IUIPresenter
{
    void OnMoveRequest();
    void OnFlipRequest();
    void OnGameOverRequest();
    void OnRestartRequest();
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
    public void OnGameOverRequest()
    {
        _uiView.TogglePopUp(SceneState.GameOver);
    }
    public void OnRestartRequest()
    {
        GameStageManager.Instance.RestartGame();
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
    #endregion

    

    
}