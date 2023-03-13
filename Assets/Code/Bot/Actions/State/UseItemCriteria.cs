using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemCriteria : MonoBehaviour, ActionStateCriteria, IUpdatableStatePriority
{
    private int priorityLevel = 0;

    public ActionState ActionState()
    {
        return global::ActionState.UseItem;
    }
    public bool PassesCriteria()
    {
        if (Global.playertracker.botInventory.HasItem(ItemName.PoisonConsumable) 
            || Global.playertracker.botInventory.HasItem(ItemName.BurstConsumable))
        {
            return true;
        }
        return false;
    }
    public int PriorityLevel()
    {
        return priorityLevel;
    }
    public void UpdatePriorityLevel()
    {
        
    }
}
