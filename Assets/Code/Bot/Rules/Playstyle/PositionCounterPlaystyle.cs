using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.playerPositionCounterLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.PlayerPositionCounter;
    }

    public void UpdatePlaystyleLevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
