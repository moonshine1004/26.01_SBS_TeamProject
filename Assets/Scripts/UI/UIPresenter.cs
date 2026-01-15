using System;
using Game.Events;

public interface IUIPresenter
{
    void OnMoveRequest();
    void OnFlipRequest();
    void OnGameOverRequest();
    void OnRestartRequest();
}

public class UIPresenter : IUIPresenter, IEventBusAware
{
    #region Constructor
    private UIModel _uiModel;
    private IUIView _uiView;
    
    public UIPresenter(UIModel uiModel, IUIView uiView)
    {
        _uiModel = uiModel;
        _uiView = uiView;
    }
    #endregion
    
    private IEventBus _eventBus;

    #region IUIPresenter Interface Implementation
    public void OnFlipRequest()
    {
        _eventBus.Publish(new OnFlipPressed());
    }

    public void OnMoveRequest()
    {
        _eventBus.Publish(new OnMovePressed());
    }
    public void OnGameOverRequest()
    {
        _uiView.TogglePopUp(SceneState.GameOver);
    }
    public void OnRestartRequest()
    {
        GameSceneManager.Instance.RestartGame();
        _eventBus.Publish(new OnRestartGame());
        _uiView.ClearPopUp();
    }
    #endregion

    public void SetEventBus(IEventBus bus)
    {
        _eventBus = bus;
    }

    
}