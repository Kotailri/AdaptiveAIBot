/// <summary>
/// Timer class for timed functionality.
/// </summary>
public class Timer
{
    private float maxTimer;
    private float currentTimer;
    private bool isTimerRunning;
    private bool isTimerPaused;
    
    public Timer(float time)
    {
        maxTimer = time;
        isTimerPaused = false;
        ResetTimer();
        Global.timerManager.AddTimer(this);
    }

    public bool IsAvailable()
    {
        return !isTimerRunning;
    }

    public float GetTimerPercent()
    {
        if (IsAvailable())
            return 1.0f;

        return (float)currentTimer / (float)maxTimer;
    }

    public void IncrementTime(float time)
    {
        if (!isTimerRunning || isTimerPaused)
            return;

        currentTimer += time;
        if (currentTimer >= maxTimer)
            isTimerRunning = false;
    }

    public void PauseTimer(bool isPaused)
    {
        isTimerPaused = isPaused;
    }

    public void ResetTimer()
    {
        currentTimer = 0;
        isTimerRunning = true;
    }
}
