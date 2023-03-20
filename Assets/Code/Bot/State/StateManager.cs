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
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        states = new List<ActionStateCriteria>();
        states.AddRange(statesCollection.GetComponents<ActionStateCriteria>());
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public ActionState ChangeStates()
    {
        stateSwapTimer = 0;
        currentState = SelectNewState();
        return currentState;
    }

    public ActionState GetCurrentState()
    {
        return currentState;
    }

    private ActionState SelectNewState()
    {
        states = states.OrderBy(state => state.PriorityLevel()).ToList();
        states.Reverse();
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].PassesCriteria())
            {
                stateSwapTimer = 0;
                stateSwapTime = states[i].StateStayTime();
                spriteRenderer.color = states[i].GetStateColor();
                if (Global.debugMode)
                    print("Swapping state to " + states[i].ActionState());
                return states[i].ActionState();
            }
        }
        Utility.PrintCol("Error: No State Selected", "FF0000");
        spriteRenderer.color = Color.white;
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
            stateSwapTimer = 0;
            ChangeStates();
            Global.playertracker.Bot.GetComponent<ActionManager>().StateChangeActions(currentState);
        }
    }

    private void Update()
    {
        UpdateState();
    }
}
