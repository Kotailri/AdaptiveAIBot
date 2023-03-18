using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ActionManager : MonoBehaviour, IResettable
{
    private List<IAction> actions = new List<IAction>();

    [HideInInspector]
    public StateManager stateManager;
    
    public bool isPaused = false;

    void Awake()
    {
        actions.AddRange(GetComponents<IAction>());
        
        stateManager = GetComponent<StateManager>();
        InitResettable();
        ExecuteActions();
    }

    public void StateChangeActions(ActionState newState)
    {
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
    }

    private void ExecuteActions()
    {
        foreach (IAction action in actions)
        {
            if (action is IActionRequiredState actionRequiredState)
            {
                if (!actionRequiredState.GetActionStates().Contains(stateManager.GetCurrentState()))
                    continue;
            }

            if (action is IActionExcludeState actionExcludeState)
            {
                if (actionExcludeState.GetExcludedActionStates().Contains(stateManager.GetCurrentState()))
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
                if (actionWithChance.GetActionChance() < actionChance)
                {
                    continue;
                }
                    
            }

            if (action is IActionHasUpdateAction actionWithUpdate)
            {
                actionWithUpdate.ExecuteAction();
            }

            if (action is IActionHasStateCompletion actionWithCompletion)
            {
                if (actionWithCompletion is IActionRequiredState actionRequiredState2)
                {
                    if (actionRequiredState2.GetActionStates().Contains(stateManager.GetCurrentState()))
                    {
                        StateChangeActions(stateManager.ChangeStates());
                    }

                }

            }
        }
    }

    void Update()
    {
        if (isPaused)
            return;
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
        stateManager.UpdateStatePriorities();
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
