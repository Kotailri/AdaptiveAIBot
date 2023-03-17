using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStratPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.itemStrategyLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.ItemStrategy;
    }

    public void SetPlaystyleLevel(int level)
    {
        Global.itemStrategyLevel = level;
    }

    public void UpdatePlaystyleLevel()
    {
        PlayerTracker tracker = Global.playertracker;
        Global.itemStrategyLevel += (int)Mathf.Clamp(tracker.PlayerItemsCollected - tracker.BotItemsCollected, -2.0f, 2.0f);
        tracker.PlayerItemsCollected = 0;
        tracker.BotItemsCollected = 0;
    }
}
