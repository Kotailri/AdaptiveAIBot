using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.playerAttackCounterLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.PlayerAttackCounter;
    }

    public void UpdatePlaystyleLevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
