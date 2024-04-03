using UnityEngine;

public class Timer
{
    private const float ResetedTimer = 0f;

    private float _timeToReady;
    private bool _isReady;
    private float _time;

    public bool IsReady => _isReady;

    public Timer(float timeToReady)
    {
        _timeToReady = timeToReady;
        _isReady = true;
        _time = ResetedTimer;
    }

    public void DecreaseTime()
    {
        if (_isReady)
            return;

        _time += Time.deltaTime;
        
        if(_time >= _timeToReady)
        {
            _isReady = true;
        }
    }

    public void Reset()
    {
        _isReady = false;
        _time = ResetedTimer;
    }
}
