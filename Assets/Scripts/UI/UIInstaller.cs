using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInstaller : MonoBehaviour
{
    [SerializeField] private IUIView _uiView;
    private UIModel _uiModel;
    private IUIPresenter _uiPresenter;

    public void Awake()
    {
        _uiView = GetComponent<IUIView>();
        _uiModel = new UIModel();
        _uiPresenter = new UIPresenter(_uiModel, _uiView); 
        _uiView.InitUIView(_uiPresenter);
        GameStageManager.Instance.InitUI(_uiPresenter);
    }
}