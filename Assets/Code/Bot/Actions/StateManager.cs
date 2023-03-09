using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private float stateSwapTimer = 0.0f;
    private float stateSwapTime = 5.0f;

    public ActionState currentState = ActionState.Wander;
    private Inventory inv;

    private void Awake()
    {
        inv = GetComponent<Inventory>();
    }

    public ActionState ChangeStates()
    {
        stateSwapTimer = 0;
        //currentState = newState;
        ActionState newState = SelectNewState();
        return ActionState.None;
    }

    public ActionState GetCurrentState()
    {
        return currentState;
    }

    private ActionState SelectNewState()
    {
        float stateChance = Random.Range(0f, 1f);
        if (stateChance > 0.6)
        {
            return ActionState.Attack;
        }

        else if (inv.HasItem(ItemName.PoisonConsumable))
        {
            return ActionState.UseItem;
        }

        else if (stateChance > 0.4 && Global.playertracker.CurrentDistance <= 10)
        {
            return ActionState.Flee;
        }

        else if (stateChance > 0.2)
        {
            return ActionState.CollectItem;
        }

        else
        {
            return ActionState.Wander;
        }
    }

    private void UpdateState()
    {
        stateSwapTimer += Time.deltaTime;

        if (stateSwapTimer >= stateSwapTime)
        {
            ChangeStates();
            stateSwapTimer = 0;
        }
    }

    private void Update()
    {
        UpdateState();
    }
}
