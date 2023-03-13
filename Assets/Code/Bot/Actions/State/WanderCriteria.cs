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
        return true;
    }

    public float StateStayTime()
    {
        return 5.0f;
    }

    public int PriorityLevel()
    {
        return -2;
    }
}
