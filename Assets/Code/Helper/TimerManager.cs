using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private List<Timer> timers = new List<Timer>();

    public void AddTimer(Timer t)
    {
        timers.Add(t);
    }

    private void Awake()
    {
        Global.timerManager = this;
    }

    void Update()
    {
        foreach(Timer t in timers)
        {
            t.IncrementTime(Time.deltaTime);
        }
    }
}
