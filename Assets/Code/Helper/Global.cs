using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Player,
    Bot,
    None
}

public enum ActionState
{ 
    Wander,
    Attack,
    Flee,
    CollectItem,
    UseItem,
    Idle,
    None
}

public enum ItemType
{
    Consumable,
    StatBoost
}

public enum ItemName
{
    PoisonConsumable,
    BurstConsumable,
    SpeedStat,
    DamageStat,
    HealStat
}

/// <summary>
/// Specifies boundaries between 2 corner vectors.
/// </summary>
public class Bounds
{
    public Vector2 botLeft;
    public Vector2 topRight;

    public Bounds(Vector2 botLeft_, Vector2 topRight_)
    {
        botLeft = botLeft_;
        topRight = topRight_;
    }

    /// <summary>
    /// Returns true if vector is within boundaries.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool IsPointInBounds(Vector2 point)
    {
        return point.x >= botLeft.x && point.x <= topRight.x &&
               point.y >= botLeft.y && point.y <= topRight.y;
    }

    /// <summary>
    /// Returns a random vector within the boundaries.
    /// </summary>
    /// <returns></returns>
    public Vector2 GenerateRandomPositionInBounds()
    {
        float randomX = Random.Range(botLeft.x, topRight.x);
        float randomY = Random.Range(botLeft.y, topRight.y);
        return new Vector2(randomX, randomY);
    }
}

public static class Utility
{
    /// <summary>
    /// Prints coloured string to console
    /// </summary>
    /// <param name="print"></param>
    /// <param name="hexkey"></param>
    public static void PrintCol(string print, string hexkey)
    {
        Debug.Log("<color=#" + hexkey + ">" + print + "</color>");
    }
}

public static class Global
{
    public static List<IResettable> resettables = new List<IResettable>();

    // Debug
    public static bool debugMode = false;

    public static GameManager gamemanager;
    public static ItemSpawner itemSpawner;
    public static PlayerTracker playertracker;
    public static DetectorManager playerDetectorManager;
    public static StatTrackerUI statTrackerUI;
    public static ItemTrackerUI itemTrackerUI;
    public static GameInfoUI gameInfoUI;
    public static RuleManager ruleManager;
    public static TimerManager timerManager;

    // Player stats
    public static float playerSpeedBoost = 0.0f;
    public static float botSpeedBoost = 0.0f;

    public static int playerDamageBoost = 0;
    public static int playerDamageBoost_big = 0;
    public static int botDamageBoost = 0;
    public static int botDamageBoost_big = 0;

    public static float difficultyLevel = 5.0f;

    // Playstyle rules
    public static int playerItemCounterLevel = 0;
    public static int itemStrategyLevel = 0;
    public static int playerAttackCounterLevel = 0;
    public static int aggressionLevel = 0;
    public static int playerPositionCounterLevel = 0;

    // Loadoptions
    public static bool isLevelupLocked = false;
    public static bool difficultyLocked = false;
    public static bool playstyleLocked = false;
    public static bool UILocked = false;
}



