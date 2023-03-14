using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;
    
    private PlayerTracker tracker;

    private Inventory botInv;
    private Inventory playerInv;

    private void Start()
    {
        tracker = Global.playertracker;
        botInv = tracker.botInventory;
        playerInv = tracker.playerInventory;
    }

    public ActionState ActionState()
    {
        return global::ActionState.UseItem;
    }

    public bool PassesCriteria()
    {
        if (botInv.GetItemCount() >= 0)
        {
            float p = Mathf.Lerp(0f, 1f, botInv.GetItemCount());
            return Random.value < p;
        }

        if (botInv.GetItemCount() > playerInv.GetItemCount())
        {
            float p = Mathf.Lerp(0f, 1f, botInv.GetItemCount() - playerInv.GetItemCount());
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
        return Mathf.Clamp(botInv.GetItemCount() - playerInv.GetItemCount(), 1.0f, 5.0f);
    }

    public void UpdatePriorityLevel()
    {
        priorityLevel = Global.itemStrategyLevel % 2;
    }
}
