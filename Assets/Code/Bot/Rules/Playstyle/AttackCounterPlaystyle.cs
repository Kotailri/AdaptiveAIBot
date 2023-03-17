using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.playerAttackCounterLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.PlayerAttackCounter;
    }

    public void SetPlaystyleLevel(int level)
    {
        Global.playerAttackCounterLevel = level;
    }

    public void UpdatePlaystyleLevel()
    {
        PlayerTracker tracker = Global.playertracker;
        if (tracker.PlayerCounterHits == 0)
        {
            Global.playerAttackCounterLevel--;
        }
        else
        {
            Global.playerAttackCounterLevel += (int)Mathf.Clamp(tracker.PlayerCounterHits/5, 1.0f, 2.0f);
        }
        
        tracker.PlayerCounterHits = 0;
    }
}
