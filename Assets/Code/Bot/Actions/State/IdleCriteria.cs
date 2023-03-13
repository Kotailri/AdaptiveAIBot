using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleCriteria : MonoBehaviour, ActionStateCriteria
{
    public ActionState ActionState()
    {
        return global::ActionState.Idle;
    }
    public bool PassesCriteria()
    {
        return Global.difficultyLevel < 0;
    }

    public float StateStayTime()
    {
        return 5.0f;
    }

    public int PriorityLevel()
    {
        return -1;
    }
}
