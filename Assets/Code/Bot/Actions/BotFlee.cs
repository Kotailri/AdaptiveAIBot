using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFlee : MonoBehaviour, IActionHasUpdateAction, IActionRequiredState
{
    public bool CheckAction()
    {
        return true;
    }

    public void ExecuteAction()
    {
        GetComponent<BotMove>().Flee();
    }

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.Flee };
    }
}
