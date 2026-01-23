using System.Collections.Generic;
using UnityEngine;
using Game.Events;

public enum SceneState
{
    Playing,
    GameOver,
    Paused
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
        EventBus.Instance.Subscribe<OnStartGame>(_ => OnStartGame());
    }   
    private void OnStartGame()
    {
        TileDrawer.Instance.OnStart();
        DrawSatge();
        StartTimer(_stageData.time);
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
    public void InitStageData()
    {
        
    }

    private void DrawSatge()
    {
        //12.75
        Vector3 lastPos = new Vector3(17, 0, 0) + _weight;
        for(int i = 0; i < _stageData.endLine; i++)
        {
            var middle = Instantiate(_stagePrefabs[Random.Range(0, _stagePrefabs.Count -1)]);
            middle.transform.position = lastPos;
            lastPos -= new Vector3(0, middle.GetComponent<Renderer>().bounds.size.y,0);
        }
        var bottom = Instantiate(_stagePrefabs[_stagePrefabs.Count-1]);
        bottom.transform.position = lastPos;
    }
    private void StartTimer(float time)
    {
        _remainingTime = Mathf.Max(0, time);
        EventBus.Instance.Publish(new OnTimeChange());
        _isTimerRunning = true;
    }
    private void UpdateTimer()
    {
        if(_isTimerRunning == false) return;
        if(_remainingTime <= 0)
        {
            GameOver();
            _isTimerRunning = false;
            return;
        }
        _remainingTime -= Time.deltaTime;
        EventBus.Instance.Publish(new OnTimeChange());
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
        ScoreManager.Instance.ResetTileScore();
        _uiPresenter.OnUpdateScoreRequest();
        StartTimer(_stageData.time);
        Time.timeScale = 1f;
        TileDrawer.Instance.OnRestart();
        _sceneState = SceneState.Playing;
    }
    public void UpdateSceneData(StageDataSO stageData)
    {
        _stageData = stageData;
        Debug.Log($"Stage Data Updated : StageID {stageData.stageID}");
    }
    

}