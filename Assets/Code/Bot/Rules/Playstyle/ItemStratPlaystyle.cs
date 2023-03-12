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

    public void UpdatePlaystyleLevel()
    {
        PlayerTracker tracker = Global.playertracker;
        Global.itemStrategyLevel += (tracker.PlayerItemsCollected - tracker.BotItemsCollected);
        tracker.PlayerItemsCollected = 0;
        tracker.BotItemsCollected = 0;
    }
}
