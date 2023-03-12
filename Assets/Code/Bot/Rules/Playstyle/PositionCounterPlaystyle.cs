using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{

    public int GetPlaystyleLevel()
    {
        return Global.playerPositionCounterLevel;
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

        if (timeDifference > 0.0f)
        {
            timeDifference = (timeDifference - 1.0f) / 11.0f;
        }
        else
        {
            timeDifference = (timeDifference + 9.0f) / 10.0f;
        }


        Global.playerPositionCounterLevel += (int) timeDifference;
    }
}
