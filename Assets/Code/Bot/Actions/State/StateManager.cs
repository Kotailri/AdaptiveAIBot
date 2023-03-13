using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class StateManager : MonoBehaviour
{
    private float stateSwapTimer = 0.0f;
    private float stateSwapTime = 5.0f;

    public ActionState currentState = ActionState.Wander;
    public GameObject statesCollection;

    private List<ActionStateCriteria> states = new List<ActionStateCriteria>();

    private void Awake()
    {
        states = new List<ActionStateCriteria>();
        states.AddRange(statesCollection.GetComponents<ActionStateCriteria>());
    }

    public ActionState ChangeStates()
    {
        stateSwapTimer = 0;
        ActionState newState = SelectNewState();
        return newState;
    }

    public ActionState GetCurrentState()
    {
        return currentState;
    }

    private ActionState SelectNewState()
    {
        states = states.OrderBy(state => -state.PriorityLevel()).ToList();

        foreach (ActionStateCriteria state in states)
        {
            if (state.PassesCriteria())
            {
                return state.ActionState();
            }
        }
        Utility.PrintCol("Error: No State Selected", "FF0000");
        return ActionState.None;
    }

    public void UpdateStatePriorities()
    {
        foreach (ActionStateCriteria state in states)
        {
            if (state is IUpdatableStatePriority updatable)
            {
                updatable.UpdatePriorityLevel();
            }
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
