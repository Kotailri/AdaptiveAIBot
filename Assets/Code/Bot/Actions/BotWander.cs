using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotWander : MonoBehaviour, IActionHasInitialAction, IActionRequiredState, IActionHasUpdateAction
{
    private BotMove botMove;

    private void Awake()
    {
        botMove = GetComponent<BotMove>();
    }

    public void ExecuteAction()
    {
        if (botMove && botMove.destinationReached)
        {
            botMove.MoveRandom();
        }
    }

    public void ExecuteInitialAction()
    {
        botMove.MoveRandom();
    }

    public ActionState GetActionState()
    {
        return ActionState.Wander;
    }
}
