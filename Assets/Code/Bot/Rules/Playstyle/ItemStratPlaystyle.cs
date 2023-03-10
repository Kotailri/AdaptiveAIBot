using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStratPlaystyle : MonoBehaviour, IPlaystyleRule
{
    public int GetPlaystyleLevel()
    {
        return Global.itemStrategyLevel;
    }

    public PlaystyleRule GetPlaystyleName()
    {
        return PlaystyleRule.ItemStrategy;
    }

    public void UpdatePlaystyleLevel()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
