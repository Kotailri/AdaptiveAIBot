using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 1;

    private PlayerTracker tracker;

    private void Start()
    {
        tracker = Global.playertracker;
    }

    public ActionState ActionState()
    {
        return global::ActionState.Flee;
    }

    public bool PassesCriteria()
    {
        if (1.0f > Random.Range(0f, 5.0f))
            return false;

        // is bot close
        if (tracker.CurrentDistance <= 3.0f)
        {
            return true;
        }

        // if bot has less stats (high diff)
        if (Global.difficultyLevel > Random.Range(0.0f, 5.0f))
        {
            float playerStats = Global.playerSpeedBoost + Global.playerDamageBoost;
            float botStats = Global.botSpeedBoost + Global.botDamageBoost;
            if (playerStats > botStats) return true;
        }

        return (1.0f > Random.Range(0f, 5.0f));
    }

    public int PriorityLevel()
    {
        return priorityLevel;
    }

    public float StateStayTime()
    {
        return 3.0f + (Mathf.Clamp(-Global.aggressionLevel / 2.0f, 0.0f, 2.0f));
    }

    public void UpdatePriorityLevel()
    {
        if (Global.aggressionLevel > 5)
        {
            priorityLevel = 0;
        }
        else
        {
            priorityLevel = 1;
        }
    }
}
