using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
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
    Idle
}

public enum ItemType
{
    Consumable,
    StatBoost
}

public static class Global
{
    public static List<IResettable> resettables = new List<IResettable>();
    public static GameManager gamemanager;
    public static PlayerTracker playertracker;
    public static StatTrackerUI statTrackerUI;

    public static float playerSpeedBoost = 0.0f;
    public static float botSpeedBoost = 0.0f;

    public static int playerDamageBoost = 0;
    public static int playerDamageBoost_big = 0;
    public static int botDamageBoost = 0;
    public static int botDamageBoost_big = 0;
}



