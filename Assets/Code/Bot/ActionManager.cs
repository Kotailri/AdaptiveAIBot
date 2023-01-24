using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private List<IAction> actions = new List<IAction>();
    public ActionState currentState = ActionState.Attack;

    void Awake()
    {
        actions.AddRange(GetComponents<IAction>());
    }

    public void ChangeStates(ActionState newState)
    {
        foreach (IAction action in actions)
        {
            action.Cleanup();
        }
        currentState = newState;
    }

    private float stateSwapTimer = 0;
    private float stateSwapTime = 2;
    private void UpdateState()
    {
        if (Global.playertracker.CurrentDistance >= 20)
        {
            stateSwapTimer = 0;
            ChangeStates(ActionState.Attack);
        }

        stateSwapTimer += Time.deltaTime;

        if (stateSwapTimer >= stateSwapTime)
        {
            float stateChance = Random.Range(0f, 1f);
            if (stateChance > 0.6)
            {
                ChangeStates(ActionState.Attack);
            }
                
            else if (stateChance > 0.2 && Global.playertracker.CurrentDistance <= 10)
            {
                ChangeStates(ActionState.Flee);
            }
                
            else
            {
                currentState = ActionState.Wander;
                GetComponent<BotMove>().MoveRandom();
            }
                

            stateSwapTimer = 0;
        }
    }

    private void ExecuteActions()
    {
        foreach (IAction action in actions)
        {
            //if (action.GetActionState() != currentState)
            //    continue;

            if (!action.CheckAction())
                continue;

            float actionChance = Random.Range(0f, 1f);
            if (actionChance < (1.0f - action.GetActionChance()))
                continue;

            action.ExecuteAction();
        }
    }

    void Update()
    {
        switch (currentState)
        { 
            case ActionState.Attack:
                GetComponent<BotMove>().Attack();
                break;

            case ActionState.Flee:
                GetComponent<BotMove>().Flee();
                break;

        }

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
