using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{

    public int GetPlaystyleLevel()
    {
        return Global.playerPositionCounterLevel;
    }

    public void SetPlaystyleLevel(int level)
    {
        Global.playerPositionCounterLevel = level;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.PlayerPositionCounter;
    }

    public void UpdatePlaystyleLevel()
    {
        List<PlayerDetector> detectors = Global.playerDetectorManager.GetSortedDetectorList();

        float timeSpentMost = detectors[0].GetTimeSpent();  
        float timeSpentOther = 0.0f;
        for(int i = 1; i < detectors.Count; i++)
        {
            timeSpentOther += detectors[i].GetTimeSpent();
        }

        float timeDifference = timeSpentMost - timeSpentOther;
        Global.playerPositionCounterLevel += Mathf.CeilToInt(timeDifference/5.0f);

    }
}
