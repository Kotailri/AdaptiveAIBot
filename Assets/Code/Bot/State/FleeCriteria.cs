using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;

    private PlayerTracker tracker;

    private void Start()
    {
        tracker = Global.playertracker;
    }

    public ActionState ActionState()
    {
        return global::ActionState.Flee;
    }

    private bool IsInLineOfSight()
    {
        GameObject player = Global.playertracker.Player;
        GameObject bot = Global.playertracker.Bot;

        Vector2 directionToPlayer = (Vector2)player.transform.position - (Vector2)bot.transform.position;
        float distanceToPlayer = Vector2.Distance(player.transform.position, bot.transform.position);

        RaycastHit2D visionBlockers = Physics2D.Raycast(player.transform.position, -directionToPlayer, distanceToPlayer, Global.gamemanager.wallLayer);
        return visionBlockers.collider == null;
    }

    public bool PassesCriteria()
    {
        if (IsInLineOfSight())
            return true;

        // if bot has less stats (high diff)
        if (Global.difficultyLevel > Random.Range(0.0f, 5.0f))
        {
            float playerStats = Global.playerSpeedBoost + Global.playerDamageBoost;
            float botStats = Global.botSpeedBoost + Global.botDamageBoost;
            if (playerStats > botStats) return true;
        }

        // is bot close
        if (tracker.CurrentDistance <= 3.0f)
        {
            return true;
        }

        return Global.aggressionLevel < Random.Range(5, 10);

    }

    public int PriorityLevel()
    {
        return priorityLevel;
    }

    public float StateStayTime()
    {
        return Mathf.Infinity;
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

    public Color GetStateColor()
    {
        return Color.cyan;
    }
}
