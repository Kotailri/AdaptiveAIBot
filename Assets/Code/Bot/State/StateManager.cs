using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public void ForceState(ActionState state, float time=0.0f)
    {
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].ActionState() == state)
            {
                stateSwapTimer = 0;
                if (time == 0.0f)
                    stateSwapTime = states[i].StateStayTime();
                else
                    stateSwapTime = time;
                spriteRenderer.color = states[i].GetStateColor();
                if (Global.debugMode)
                    print("Forced state to " + state);
                currentState = state;
                break;
            }
        }
        Global.playertracker.Bot.GetComponent<ActionManager>().StateChangeActions(currentState);
    }

    private ActionState SelectNewState()
    {
        states = states.OrderBy(state => state.PriorityLevel()).ToList();
        states.Reverse();
        for (int i = 0; i < states.Count; i++)
        {
            if (states[i].PassesCriteria() && Random.Range(0f, 1f) >= 0.1)
            {
                stateSwapTimer = 0;
                stateSwapTime = states[i].StateStayTime();
                spriteRenderer.color = states[i].GetStateColor();
                if (Global.debugMode)
                    print("Swapping state to " + states[i].ActionState());
                return states[i].ActionState();
            }
        }
        stateSwapTimer = 0;
        stateSwapTime = states[0].StateStayTime();
        spriteRenderer.color = states[0].GetStateColor();
        if (Global.debugMode)
            print("Swapping state to " + states[0].ActionState());
        return states[0].ActionState();
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
