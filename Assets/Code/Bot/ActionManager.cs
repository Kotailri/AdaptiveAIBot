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

    // Update is called once per frame
    void Update()
    {
        #region Debug State Swapper
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

        foreach (IAction action in actions)
        {
            if (action.GetActionState() != currentState)
                continue;

            if (!action.CheckAction())
                continue;

            float actionChance = Random.Range(0f, 1f);
            if (actionChance < (1.0f - action.GetActionChance()))
                continue;

            action.ExecuteAction();
        }
    }
}
