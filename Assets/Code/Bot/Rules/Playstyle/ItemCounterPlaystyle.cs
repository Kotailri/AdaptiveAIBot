using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCounterPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.playerItemCounterLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.PlayerItemCounter;
    }

    public void UpdatePlaystyleLevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
