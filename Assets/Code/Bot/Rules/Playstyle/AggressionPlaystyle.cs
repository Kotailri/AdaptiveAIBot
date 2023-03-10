using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressionPlaystyle : MonoBehaviour, IPlaystyleRule
{
    private PlayerTracker playerTracker;
    private float approachTimer = 0.0f;

    private void Awake()
    {
        playerTracker = GetComponent<PlayerTracker>();
    }

    private void Update()
    {
        if (!playerTracker)
            return;

        if (playerTracker.CurrentDistance < GameConfig.c_AggroApproachDist)
        {
            approachTimer += Time.deltaTime;
        }
        else
        {
            approachTimer -= Time.deltaTime;

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
        Global.aggressionLevel += (int) Mathf.Clamp(approachTimer, -10.0f, 10.0f);
        approachTimer = 0.0f;
    }
}
