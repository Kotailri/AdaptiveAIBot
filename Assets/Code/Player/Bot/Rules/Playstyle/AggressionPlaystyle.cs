using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressionPlaystyle : MonoBehaviour, IPlaystyleRule
{
    private PlayerTracker playerTracker;
    private float approachTimer = 0.0f;
    private float totalRoundTimer = 0.0f;

    private void Start()
    {
        playerTracker = Global.playertracker;
    }

    public void SetPlaystyleLevel(int level)
    {
        Global.aggressionLevel = level;
    }
    
    private void Update()
    {
        

        if (!playerTracker)
            return;

        if (playerTracker.Bot.GetComponent<ActionManager>().stateManager.GetCurrentState() != ActionState.Attack)
        {
            totalRoundTimer += Time.deltaTime;
            if (playerTracker.CurrentDistance < GameConfig.c_AggroApproachDist)
            {
                approachTimer += Time.deltaTime;
            }
            else
            {
                approachTimer -= Time.deltaTime;
            }
        }

        
    }

    public int GetPlaystyleLevel()
    {
        return Global.aggressionLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.Aggression;
    }

    public void UpdatePlaystyleLevel()
    {
        Global.aggressionLevel += (int) Mathf.Clamp((approachTimer/totalRoundTimer)*2, -2.0f, 2.0f);
        approachTimer = 0.0f;
        totalRoundTimer = 0.0f;
    }
}
