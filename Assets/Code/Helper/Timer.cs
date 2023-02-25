public class Timer
{
    private float maxTimer;
    private float currentTimer;

    private bool timerRunning;

    public Timer(float time)
    {
        maxTimer = time;
        ResetTimer();
        Global.timerManager.AddTimer(this);
    }

    public bool IsAvailable()
    {
        return !timerRunning;
    }

    public void IncrementTime(float time)
    {
        if (!timerRunning)
            return;

        currentTimer += time;
        if (currentTimer >= maxTimer)
            timerRunning = false;
    }

    public void ResetTimer()
    {
        currentTimer = 0;
        timerRunning = true;
    }
}
