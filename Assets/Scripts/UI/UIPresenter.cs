

public class UIPresenter
{
    private UIModel _uiModel;
    private UIView _uiView;
    
    public UIPresenter(UIModel uiModel, UIView uiView)
    {
        _uiModel = uiModel;
        _uiView = uiView;
    }
}