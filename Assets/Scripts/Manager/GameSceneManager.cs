using UnityEngine;

public enum SceneState
{
    Playing,
    GameOver,
    Paused

}

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager _instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameSceneManager>();
            }
            return _instance;
        }
    }

    [SerializeField] private IUIPresenter _uiPresenter;

    private SceneState _sceneState = SceneState.Playing;



    #region Unity Lifecycle
    private void Awake()
    {   
        
    }
    private void Start()
    {
        TileDrawer.Instance.OnStart();
    }
    private void Update()
    {
        
    }
    #endregion
    public void InitUI(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }

    public void GameOver()
    {
        if (_sceneState == SceneState.GameOver) return;
        _sceneState = SceneState.GameOver;
        Time.timeScale = 0f;
        _uiPresenter.OnGameOverRequest();

    }
    public void RestartGame()
    {
        if (_sceneState != SceneState.GameOver) return;
        Time.timeScale = 1f;
        TileDrawer.Instance.OnRestart();


        _sceneState = SceneState.Playing;
    }

}