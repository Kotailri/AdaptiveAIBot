using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.playerItemCounterLevel;
    }

    public void SetPlaystyleLevel(int level)
    {
        Global.playerItemCounterLevel = level;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.PlayerItemCounter;
    }

    public void UpdatePlaystyleLevel()
    {
        PlayerTracker tracker = Global.playertracker;
        Global.playerItemCounterLevel += (int)Mathf.Clamp(tracker.PlayerItemsUsed - tracker.BotItemsUsed, -2f, 2f);
        tracker.BotItemsUsed = 0;
        tracker.PlayerItemsUsed = 0;
    }
}
