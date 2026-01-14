using System;
using Game.Events;

public interface IUIPresenter
{
    void OnMove();
    void OnFlip();
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


    public void OnFlip()
    {
        _eventBus.Publish(new OnFlipPressed());
    }

    public void OnMove()
    {
        _eventBus.Publish(new OnMovePressed());
    }

    public void SetEventBus(IEventBus bus)
    {
        _eventBus = bus;
    }
}