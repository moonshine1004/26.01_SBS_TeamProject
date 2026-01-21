using System.Collections.Generic;
using UnityEngine;
using Game.Events;

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
        get => _remainingTime;
    }
    #endregion




    #region Unity Lifecycle
    private void Awake()
    {   


    }   
    private void Start()
    {
        TileDrawer.Instance.OnStart();
        DrawSatge();
        StartTimer(600);
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
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
        var top = Instantiate(_stagePrefabs[0]);
        top.transform.position = new Vector3(17, 10.4f, 0);
        Vector3 lastPos = top.transform.position - new Vector3(0, 10.4f,0) + _weight;
        for(int i = 0; i < _sceneData.endLine; i++)
        {
            var middle = Instantiate(_stagePrefabs[Random.Range(1, _stagePrefabs.Count -1)]);
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
        Time.timeScale = 1f;
        TileDrawer.Instance.OnRestart();
        _sceneState = SceneState.Playing;
    }
    

}