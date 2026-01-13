

public class RuntimeManager
{
    private static RuntimeManager _instance;
    public static RuntimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RuntimeManager();
            }
            return _instance;
        }
    }

    private IPlayerPresenter _playerPresenter;
    private IUIPresenter _uiPresenter;

    public void SetPlayerPresenter(IPlayerPresenter playerPresenter)
    {
        _playerPresenter = playerPresenter;
    }
    public IPlayerPresenter GetPlayerPresenter()
    {
        return _playerPresenter;
    }
    public void SetUIPresenter(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }
    public IUIPresenter GetUIPresenter()
    {
        return _uiPresenter;
    } 
}