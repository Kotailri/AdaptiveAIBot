using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderCriteria : MonoBehaviour, ActionStateCriteria
{
    public ActionState ActionState()
    {
        return global::ActionState.Wander;
    }
    public bool PassesCriteria()
    {
        return Random.Range(0, 5) > 1;
    }

    public float StateStayTime()
    {
        return -(int)Global.difficultyLevel;
    }

    public int PriorityLevel()
    {
        return -(int)Global.difficultyLevel;
    }

    public Color GetStateColor()
    {
        return Color.blue;
    }
}
