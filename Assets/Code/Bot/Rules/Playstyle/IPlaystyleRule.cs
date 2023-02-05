using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaystyleRule
{
    public string GetPlaystyleName();
    public int GetPlaystyleChance();
    public int GetPlaystyleLevel();
    public void UpdatePlaystyleLevel(int levelUp);
}
