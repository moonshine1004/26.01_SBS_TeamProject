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

    #region Unity Lifecycle
    private void Awake()
    {

    }
    #endregion

    private int _tileScore = 1;
    private int _currentScore;

    public int TileScore
    {
        get
        {
            return _tileScore;
        }

    }
    public int CurrentScore
    {
        get
        {
            _currentScore = _currentScore = ScoreUtil.CalculateClearScore(TileScore, GameStageManager.Instance.RemainingTime, GameManager.Instance.CurrentPlayerData.bonusRate);
            return _currentScore;
        }
    }
    public int HighScore
    {
        get
        {
            if(CurrentScore > GamePrefsRepository.CurrentHighScore)
            {
                GamePrefsRepository.CurrentHighScore = CurrentScore;
            }
            return GamePrefsRepository.CurrentHighScore;
        } 
    }

    public void AddTileScore()
    {
        _tileScore++;
    }
    public void ResetTileScore()
    {
        _tileScore = 1;
    }
}
