using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlaystyleRule
{ 
    Aggression,
    PlayerPositionCounter,
    ItemStrategy,
    PlayerItemCounter,
    PlayerAttackCounter
}

public interface IPlaystyleRule
{
    public PlaystyleRule GetPlaystyleName();
    public void SetPlaystyleLevel(int level);
    public void UpdatePlaystyleLevel();
    public int GetPlaystyleLevel();
}
