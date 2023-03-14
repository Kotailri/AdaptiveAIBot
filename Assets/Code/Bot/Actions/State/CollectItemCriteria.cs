using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;

    private PlayerTracker tracker;
    private BotAreaScanner scanner;

    private Inventory botInv;
    private Inventory playerInv;

    private void Start()
    {
        tracker = Global.playertracker;
        botInv = tracker.botInventory;
        playerInv = tracker.playerInventory;
        scanner = tracker.Bot.GetComponent<BotAreaScanner>();
    }

    public ActionState ActionState()
    {
        return global::ActionState.CollectItem;
    }

    public bool PassesCriteria()
    {
        if (botInv.GetItemCount() == 0)
        {
            float p = 1f - Mathf.Lerp(0f, 1f, scanner.DistanceToNearestItem());
            return Random.value < p;
        }

        if (playerInv.GetItemCount() > botInv.GetItemCount())
        {
            float p = Mathf.Lerp(0f, 1f, playerInv.GetItemCount() - botInv.GetItemCount());
            return Random.value < p;
        }

        return 4.0f > Random.Range(0f, 5.0f);
    }

    public int PriorityLevel()
    {
        return priorityLevel;
    }

    public float StateStayTime()
    {
        return Mathf.Clamp(playerInv.GetItemCount() - botInv.GetItemCount(), 1.0f, 5.0f);
    }

    public void UpdatePriorityLevel()
    {
        priorityLevel = Global.playerItemCounterLevel % 2;
    }
}
