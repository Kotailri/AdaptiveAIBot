using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActionManager : MonoBehaviour, IResettable
{
    private List<IAction> actions = new List<IAction>();
    public ActionState currentState = ActionState.Wander;

    private Inventory inv;
    public bool isPaused = false;

    void Awake()
    {
        actions.AddRange(GetComponents<IAction>());
        inv = GetComponent<Inventory>();
        InitResettable();
        ExecuteActions();
    }

    public void ChangeStates(ActionState newState)
    {
        print("State Change: " + newState);
        foreach (IAction action in actions)
        {
            if (action is IActionHasCleanup cleanupAction)
            {
                cleanupAction.Cleanup();
            }
                
            if (action is IActionHasInitialAction initialAction)
            {
                if (initialAction.GetActionStates().Contains(newState))
                {
                    initialAction.ExecuteInitialAction();
                }
            }
                
        }
        currentState = newState;
    }

    private float stateSwapTimer = 0.0f;
    private float stateSwapTime = 5.0f;

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
            ChangeStates(SelectNewState());
            stateSwapTimer = 0;
        }
    }

    private void ExecuteActions()
    {
        foreach (IAction action in actions)
        {
            if (action is IActionRequiredState actionRequiredState)
            {
                if (!actionRequiredState.GetActionStates().Contains(currentState))
                    continue;
            }

            if (action is IActionExcludeState actionExcludeState)
            {
                if (actionExcludeState.GetExcludedActionStates().Contains(currentState))
                    continue;
            }

            if (action is IActionHasActionCheck actionWithCheck)
            {
                if (actionWithCheck.CheckAction() == false)
                    continue;
            }

            if (action is IActionHasActionChance actionWithChance)
            {
                float actionChance = Random.Range(0f, 1f);
                if (actionChance < (1.0f - actionWithChance.GetActionChance()))
                    continue;
            }

            if (action is IActionHasUpdateAction actionWithUpdate)
            {
                actionWithUpdate.ExecuteAction();
            }

            if (action is IActionHasStateCompletion actionWithCompletion)
            {
                if (actionWithCompletion.IsStateComplete())
                {
                    ChangeStates(SelectNewState());
                    stateSwapTimer = 0;
                }
                    
            }
        }
    }

    void Update()
    {
        if (isPaused)
            return;

        UpdateState();
        ExecuteActions();
    }

    public void ResetObject()
    {
        foreach (IAction action in actions)
        {
            if (action is IActionHasCleanup cleanupAction)
            {
                cleanupAction.Cleanup();
            }
        }
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }
}
