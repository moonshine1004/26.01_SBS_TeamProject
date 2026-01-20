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

    public void StartTimer(float time)
    {
        _remainingTime = Mathf.Max(0, time);
        EventBus.Instance.Publish<OnTimeChange>(new OnTimeChange());
        _isRunning = true;
    }

}