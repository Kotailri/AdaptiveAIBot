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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Global.playertracker.Bot.GetComponent<ActionManager>().stateManager.GetCurrentState() != ActionState.Wander)
            return;

        BulletCollision bulletCollision;
        if (collision.gameObject.TryGetComponent<BulletCollision>(out bulletCollision))
        {
            if (bulletCollision.playerType == PlayerType.Player)
            {
                Global.playertracker.PlayerHitsDuringWander++;
            }
        }
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

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.Wander };
    }
}
