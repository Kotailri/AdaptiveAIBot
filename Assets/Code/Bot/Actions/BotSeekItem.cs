using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSeekItem : MonoBehaviour, IActionHasInitialAction, IActionRequiredState, IActionHasUpdateAction, IActionHasStateCompletion
{
    private BotMove botMove;
    private BotAreaScanner scanner;
    private bool completed = false;
    private void Awake()
    {
        botMove = GetComponent<BotMove>();
        scanner = GetComponent<BotAreaScanner>();
    }

    private bool ItemsAvailable()
    {
        return Global.itemSpawner.currentItems.Count > 0;
    }

    private void CollectItem()
    {
        if (ItemsAvailable())
        {
            Vector2 itemLocation = scanner.LocateNearestItem();
            botMove.SetMove(itemLocation.x, itemLocation.y);
        }
        else
        {
            completed = true;
        }
    }

    public void ExecuteAction()
    {
        if (botMove.destinationReached && !completed)
        {
            completed = true;
            print("done!");
        }
    }

    public void ExecuteInitialAction()
    {
        completed = false;
        CollectItem();
    }

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.CollectItem };
    }

    public bool IsStateComplete()
    {
        return completed;
    }
}
