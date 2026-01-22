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
    [SerializeField] private StageDataSO _sceneData; // 스테이지 정보가 담긴 스크립터블 오브젝트 -> 추후 게임 매니저가 게팅하도록 수정 요망
    private SceneState _sceneState = SceneState.Playing;
    [SerializeField] private float _remainingTime;
    private bool _isTimerRunning = false;
    public float RemainingTime
    {
        get => (int)_remainingTime;
    }
    #endregion

    public int TestTime = 1000;


    #region Unity Lifecycle
    private void Awake()
    {   


    }   
    private void Start()
    {
        TileDrawer.Instance.OnStart();
        DrawSatge();
        StartTimer(TestTime);
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
        Vector3 lastPos = new Vector3(17, 0, 0) + _weight;
        for(int i = 0; i < _sceneData.endLine; i++)
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
        StartTimer(TestTime);
        Time.timeScale = 1f;
        TileDrawer.Instance.OnRestart();
        _sceneState = SceneState.Playing;
    }
    

}