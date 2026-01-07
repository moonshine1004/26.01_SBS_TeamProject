using UnityEngine;

public class UIInstaller : MonoBehaviour
{
    [SerializeField] private UIView _uiView;
    private UIModel _uiModel;
    private UIPresenter _uiPresenter;

    public void Awake()
    {
        var _uiModel = new UIModel();
        var _uiPresenter = new UIPresenter(_uiModel, _uiView); 
        _uiView.InitUIView(_uiPresenter);
    }
}