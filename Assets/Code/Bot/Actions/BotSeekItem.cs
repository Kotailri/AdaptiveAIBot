using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotSeekItem : MonoBehaviour, IActionHasInitialAction, IActionRequiredState, IActionHasUpdateAction
{
    private BotMove botMove;

    private void Awake()
    {
        botMove = GetComponent<BotMove>();
    }

    private bool ItemsAvailable()
    {
        return Global.itemSpawner.currentItems.Count > 0;
    }

    private Vector2 LocateNearestItem()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;
        foreach (GameObject gameObject in Global.itemSpawner.currentItems)
        {
            float distance = Vector2.Distance(gameObject.transform.position, transform.position);
            if (distance < minDistance)
            {
                closest = gameObject;
                minDistance = distance;
            }
        }
        return closest.transform.position;
    }

    private void CollectItem()
    {
        if (ItemsAvailable())
        {
            Vector2 itemLocation = LocateNearestItem();
            botMove.SetMove(itemLocation.x, itemLocation.y);
        }
        else
        {
            botMove.MoveRandom();
        }
    }

    public void ExecuteAction()
    {
        if (botMove.destinationReached)
        {
            CollectItem();
        }
    }

    public void ExecuteInitialAction()
    {
        CollectItem();
    }

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.CollectItem };
    }
}
