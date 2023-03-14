using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;

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

        return 4.0f > Random.Range(0f, 5.0f);
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
