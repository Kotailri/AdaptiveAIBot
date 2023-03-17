using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 1;

    private PlayerTracker tracker;

    private void Start()
    {
        tracker = Global.playertracker;
    }

    public ActionState ActionState()
    {
        return global::ActionState.Attack;
    }

    public bool PassesCriteria()
    {
        if (1.0f > Random.Range(0f, 5.0f))
            return false;

        // is the bot far away
        if (tracker.CurrentDistance >= 8.0f)
        {
            return true;
        }

        // if bot has more stats (high diff)
        if (Global.difficultyLevel > Random.Range(0.0f, 5.0f))
        {
            float playerStats = Global.playerSpeedBoost + Global.playerDamageBoost;
            float botStats = Global.botSpeedBoost + Global.botDamageBoost;
            if (playerStats < botStats) return true;
        }

        return false;
    }

    public int PriorityLevel()
    {
        return priorityLevel;
    }

    public float StateStayTime()
    {
        return 3.0f + (Mathf.Clamp((float)Global.aggressionLevel/2.0f, 0.0f, 5.0f));
    }

    public void UpdatePriorityLevel()
    {
        if (Global.aggressionLevel > 5)
        {
            priorityLevel = 1;
        }
        else
        {
            priorityLevel = 0;
        }
    }
}
