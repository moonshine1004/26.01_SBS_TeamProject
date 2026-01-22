using System;
using UnityEngine;
using Game.Events;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<ScoreManager>();
            }
            return _instance;
        }
    }

    private IDisposable _updateScore; 

    #region Unity Lifecycle
    private void Awake()
    {
        // 점수 불러오기 기능 추가 필요
        _updateScore = EventBus.Instance.Subscribe<OnUpdateTileScore>(_ => AddTileScore());
    }
    #endregion

    private int _tileScore;
    private int _currentScore;
    private int _highScore;

    public int TileScore
    {
        get => _tileScore;
    }
    public int CurrentScore
    {
        get
        {
            _currentScore = _currentScore = ScoreUtil.CalculateClearScore(TileScore, GameStageManager.Instance.RemainingTime, 1);
            return _currentScore;
        }
    }
    public int HighScore
    {
        get
        {
            if(CurrentScore > _highScore)
            {
                _highScore = CurrentScore;
            }
            return _highScore;
        } 
    }

    public void AddTileScore()
    {
        _tileScore++;
    }
    public void ResetTileScore()
    {
        _tileScore = 0;
    }
}
