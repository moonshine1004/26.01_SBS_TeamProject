using System;
using UnityEngine;
using Game.Events;

public enum TimerState
{
    Idle, 
    Running, 
    Paused,
    Finished
}

public class StageTimer : MonoBehaviour
{
    private float _remainingTime;
    private bool _isRunning = false;

    #region Unity Lifecycle
    private void Update()
    {
        if(_isRunning == false) return;

        _remainingTime -= Time.deltaTime;

        if(_remainingTime <= 0f)
        {
            _remainingTime = 0;
            _isRunning = false;
            EventBus.Instance.Publish<OnTimeOver>(new OnTimeOver());
        }
    }
    #endregion


    public void StartTimer(float time)
    {
        _remainingTime = Mathf.Max(0, time);
        EventBus.Instance.Publish<OnTimeChange>(new OnTimeChange());
        _isRunning = true;
    }
    public void StopTimer()
    {
        _isRunning = false;
    }

}