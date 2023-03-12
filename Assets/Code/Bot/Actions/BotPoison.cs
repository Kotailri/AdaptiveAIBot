using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotPoison : MonoBehaviour, IActionRequiredState, IActionHasInitialAction, IActionHasStateCompletion, IActionHasUpdateAction
{
    public GameObject poison;

    private BotMove botMove;
    private DetectorManager detectorManager;
    private Inventory inv;

    private bool completed = false;

    private void Start()
    {
        botMove = GetComponent<BotMove>();
        detectorManager = Global.playerDetectorManager;
        inv = GetComponent<Inventory>();
    }

    public void ExecuteAction()
    {
        if (botMove && botMove.destinationReached && !completed)
        {
            if (inv.ConsumeItem(ItemName.PoisonConsumable))
            {
                Instantiate(poison, transform.position, Quaternion.identity);
                Global.playertracker.BotItemsUsed++;
            }
            botMove.Stop();
            completed = true;
        }   
    }

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.UseItem };
    }

    public void ExecuteInitialAction()
    {
        completed = false;
        Transform poisonPos = detectorManager.GetSortedDetectorList()[0].GetPoisonPosition();
        float variance = (Global.difficultyLevel - 5) / 2;
        botMove.SetMove(poisonPos.position.x, poisonPos.position.y, variance: -variance);
    }

    public bool IsStateComplete()
    {
        return completed;
    }
}
