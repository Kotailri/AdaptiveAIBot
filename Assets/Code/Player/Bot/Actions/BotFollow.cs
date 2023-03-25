using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFollow : MonoBehaviour, IActionHasUpdateAction, IActionRequiredState
{
    public void ExecuteAction()
    {
        GetComponent<BotMove>().Attack();
    }

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.Attack };
    }
}
