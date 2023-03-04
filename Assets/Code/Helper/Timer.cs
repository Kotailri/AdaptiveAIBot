public class Timer
{
    private float maxTimer;
    private float currentTimer;
    private bool isTimerRunning;
    
    public Timer(float time)
    {
        maxTimer = time;
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
        if (!isTimerRunning)
            return;

        currentTimer += time;
        if (currentTimer >= maxTimer)
            isTimerRunning = false;
    }

    public void ResetTimer()
    {
        currentTimer = 0;
        isTimerRunning = true;
    }
}
