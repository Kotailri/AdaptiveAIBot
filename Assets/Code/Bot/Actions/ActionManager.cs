using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private List<IAction> actions = new List<IAction>();
    public ActionState currentState = ActionState.Wander;

    void Awake()
    {
        actions.AddRange(GetComponents<IAction>());
        ExecuteActions();
    }

    public void ChangeStates(ActionState newState)
    {
        foreach (IAction action in actions)
        {
            if (action is IActionHasCleanup cleanupAction)
            {
                cleanupAction.Cleanup();
            }
                
            if (action is IActionHasInitialAction initialAction)
            {
                if (newState == initialAction.GetActionState())
                {
                    initialAction.ExecuteInitialAction();
                }
            }
                
        }
        currentState = newState;
    }

    private float stateSwapTimer = 0.0f;
    private float stateSwapTime = 5.0f;
    private void UpdateState()
    {
        stateSwapTimer += Time.deltaTime;

        if (stateSwapTimer >= stateSwapTime)
        {
            float stateChance = Random.Range(0f, 1f);
            if (stateChance > 0.7)
            {
                ChangeStates(ActionState.Attack);
            }
                
            else if (stateChance > 0.4 && Global.playertracker.CurrentDistance <= 10)
            {
                ChangeStates(ActionState.Flee);
            }
                
            else if (stateChance > 0.2)
            {
                ChangeStates(ActionState.CollectItem);
            }

            else
            {
                ChangeStates(ActionState.Wander);
            }
                
            stateSwapTimer = 0;
        }
    }

    private void ExecuteActions()
    {
        foreach (IAction action in actions)
        {
            if (action is IActionRequiredState actionRequiredState)
            {
                if (actionRequiredState.GetActionState() != currentState)
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
        }
    }

    void Update()
    {
        #region State Swapper Test
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeStates(ActionState.Wander);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeStates(ActionState.Attack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeStates(ActionState.Flee);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeStates(ActionState.CollectItem);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeStates(ActionState.UseItem);
        }
        #endregion
        UpdateState();
        ExecuteActions();
    }
}
