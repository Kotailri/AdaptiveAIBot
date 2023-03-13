using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;
    public ActionState ActionState()
    {
        return global::ActionState.Attack;
    }

    public bool PassesCriteria()
    {
        return true;
    }

    public int PriorityLevel()
    {
        return priorityLevel;
    }

    public void UpdatePriorityLevel()
    {
        if (Global.aggressionLevel >= 0)
        {
            priorityLevel = Global.aggressionLevel % 5;
        }
        else
        {
            priorityLevel = -(Mathf.Abs(Global.aggressionLevel) % 5);
        }
    }
}
