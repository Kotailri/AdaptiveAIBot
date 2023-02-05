using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDifficultyRule
{
    public string GetDifficultyActionName();
    public int GetDifficultyActionChance();
    public int GetDifficultyActionLevel();
    public void UpdateDifficultyActionLevel(int levelUp);
}
