using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFollow : MonoBehaviour, IActionHasUpdateAction, IActionRequiredState
{
    public void ExecuteAction()
    {
        GetComponent<BotMove>().Attack();
    }

    public ActionState GetActionState()
    {
        return ActionState.Attack;
    }
}
