

using System;

public interface IUIPresenter
{
    void OnMove();
    event Action OnMoveInput;
    void OnFlip();
    event Action OnFlipInput;
}

public class UIPresenter : IUIPresenter
{
    private UIModel _uiModel;
    private UIView _uiView;
    
    public UIPresenter(UIModel uiModel, UIView uiView)
    {
        _uiModel = uiModel;
        _uiView = uiView;
        RuntimeManager.Instance.SetUIPresenter(this);
    }

    public event Action OnMoveInput;
    public event Action OnFlipInput;

    public void OnFlip()
    {
        OnFlipInput.Invoke();
    }

    public void OnMove()
    {
        OnMoveInput.Invoke();
    }
    
}