using UnityEngine;

public class UIInstaller : MonoBehaviour
{
    [SerializeField] private IUIView _uiView;
    private UIModel _uiModel;
    private IUIPresenter _uiPresenter;

    [SerializeField] private EventBus _eventBus;

    public void Awake()
    {
        _uiView = GetComponent<IUIView>();
        _uiModel = new UIModel();
        _uiPresenter = new UIPresenter(_uiModel, _uiView); 
        _uiView.InitUIView(_uiPresenter);
        if (_uiPresenter is IEventBusAware busAware)
        {
            busAware.SetEventBus(_eventBus);
        }
    }
}