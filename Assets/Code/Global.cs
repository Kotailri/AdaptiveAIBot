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
    UseItem
}


public static class Global
{
    public static List<IResettable> resettables = new List<IResettable>();
    public static GameManager gamemanager;
}



