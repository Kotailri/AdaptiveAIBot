using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;

    public ActionState ActionState()
    {
        return global::ActionState.Flee;
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
        
    }
}
