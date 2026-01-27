using System.Collections.Generic;
using UnityEngine;
using Game.Events;
using UnityEngine.SocialPlatforms.Impl;

public enum SceneState
{
    Playing,
    GameOver,
    GameClear,
    Paused
}
public interface IGameStageManager
{
    void OnUpdateSceneData(StageDataSO stageData);
    void OnSceneStateChange(SceneState sceneState);
}

public class GameStageManager : MonoBehaviour
{
    private static GameStageManager _instance;
    public static GameStageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameStageManager>();
            }
            return _instance;
        }
    }
    #region Components
    [SerializeField] private IUIPresenter _uiPresenter;

    [SerializeField] private List<GameObject> _stagePrefabs;
    #endregion

    #region Feilds
    [SerializeField] private Vector3 _weight = new(); // 두 번째 배경 이후의 위치값을 위한 가중치 
    [SerializeField] private StageDataSO _stageData; 
    private SceneState _sceneState = SceneState.Playing;
    [SerializeField] private float _remainingTime;
    private bool _isTimerRunning = false;
    public float RemainingTime
    {
        get => (int)_remainingTime;
    }
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {   
        EventBus.Instance.Subscribe<OnStartGame>(_ => SetStartGame());
        EventBus.Instance.Subscribe<OnRestartGame>(_ => RestartGame());
        EventBus.Instance.Subscribe<OnReturnLobby>(_ => SetReturnLobby());
    }   
    private void Update()
    {
        UpdateTimer();
    }
    #endregion
    public void InitUI(IUIPresenter uiPresenter)
    {
        _uiPresenter = uiPresenter;
    }

    #region Game Stage Utils
    private void DrawSatge()
    {
        ClearStage();
        Vector3 lastPos = new Vector3(17, 0, 0) + _weight;
        for(int i = 0; i < _stageData.endLine - 1; i++)
        {
            var middle = Instantiate(_stagePrefabs[Random.Range(0, _stagePrefabs.Count -1)]);
            middle.transform.position = lastPos;
            lastPos -= new Vector3(0, middle.GetComponent<Renderer>().bounds.size.y,0);
        }
        var bottom = Instantiate(_stagePrefabs[_stagePrefabs.Count-1]);
        bottom.transform.position = lastPos + new Vector3(0.25f, -1.92f, 0);
    }
    private void ClearStage()
    {
        var stages = GameObject.FindGameObjectsWithTag("Stage");
        foreach(var stage in stages)
        {
            Destroy(stage);
        }
    }
    private void StartTimer(float time)
    {
        _remainingTime = Mathf.Max(0, time + ConstVariable.startAnimationTime);
        EventBus.Instance.Publish(new OnTimeChange());
        _isTimerRunning = true;
    }
    private void UpdateTimer()
    {
        if(_isTimerRunning == false) return;
        if(_remainingTime <= 0)
        {
            SetGameOver();
            _isTimerRunning = false;
            return;
        }
        _remainingTime -= Time.deltaTime;
        EventBus.Instance.Publish(new OnTimeChange());
    }
    #endregion


    public void UpdateSceneData(StageDataSO stageData)
    {
        SetSceneData(stageData);
    }
    public void OnSceneStateChange(SceneState sceneState)
    {
        switch (sceneState)
        {
            case SceneState.GameOver:
                SetGameOver();
                break;
            case SceneState.GameClear:
                SetGameClear();
                break;
            case SceneState.Paused:
                Time.timeScale = 0f;
                break;
            default:
                break;
        }
    }
    #region Game Stage Stage Methods
    private void SetSceneData(StageDataSO stageData)
    {
        _stageData = stageData;
    }
    private void SetStartGame()
    {
        _sceneState = SceneState.Playing;
        TileDrawer.Instance.OnStart();
        DrawSatge();
        StartTimer(_stageData.time);
        ScoreManager.Instance.ResetTileScore();
    }
    private void SetGameOver()
    {
        if (_sceneState == SceneState.GameOver) return;
        _sceneState = SceneState.GameOver;
        Time.timeScale = 0f;
        
        EventBus.Instance.Publish(new OnGameOver());
    }
    private void SetGameClear()
    {
        if (_sceneState == SceneState.GameClear) return;
        _sceneState = SceneState.GameClear;
        _isTimerRunning = false;
        Time.timeScale = 0f;

        EventBus.Instance.Publish(new OnGameClear());
    }
    private void RestartGame()
    {
        ScoreManager.Instance.ResetTileScore();
        _uiPresenter.OnUpdateScoreRequest();
        StartTimer(_stageData.time);
        Time.timeScale = 1f;
        TileDrawer.Instance.OnStart();
        _sceneState = SceneState.Playing;
    }
    private void SetReturnLobby()
    {
        Time.timeScale = 1f;
        _sceneState = SceneState.Playing;
        _isTimerRunning = false;

    }
    #endregion
    

}