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
        // 점수 불러오기
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
    public int HighScore
    {
        get
        {
            if(_tileScore > _highScore)
            {
                _highScore = _tileScore;
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
