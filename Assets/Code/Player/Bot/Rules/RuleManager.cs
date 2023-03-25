using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour
{
    public GameObject DifficultyManager;
    public GameObject PlaystyleManager;

    private List<IDifficultyRule> difficultyRules;
    private List<IPlaystyleRule> playstyleRules;

    private void Start()
    {
        Global.ruleManager = this;
        difficultyRules = new List<IDifficultyRule>();
        difficultyRules.AddRange(DifficultyManager.GetComponents<IDifficultyRule>());
        
        playstyleRules = new List<IPlaystyleRule>();
        playstyleRules.AddRange(PlaystyleManager.GetComponents<IPlaystyleRule>());
    }

    public void UpdatePlaystyle()
    {
        if (Global.isLevelupLocked || Global.playstyleLocked)
        {
            if (Global.debugMode)
                print("Playstyle Locked");
            return;
        }

        foreach (IPlaystyleRule rule in playstyleRules)
        {
            rule.UpdatePlaystyleLevel();
            rule.SetPlaystyleLevel(Mathf.Clamp(rule.GetPlaystyleLevel(), GameConfig.minPlaystyleLevel, GameConfig.maxPlaystyleLevel));

            if (Global.debugMode)
                print(rule.GetPlaystyleName() + ": " + rule.GetPlaystyleLevel());
        }

        if (Global.debugMode)
            Utility.PrintCol("==========================", "00FF00");
    }

    public void UpdateDifficulty(PlayerType winner)
    {
        if (Global.isLevelupLocked || Global.difficultyLocked)
        {
            if (Global.debugMode)
                print("Difficulty Locked");
            return;
        }

        foreach (IDifficultyRule rule in difficultyRules)
        {
            float difficultyChange = rule.GetDifficultyLevelChange(winner);
            if (difficultyChange > GameConfig.c_MaxDifficultyIncrease)
            {
                difficultyChange = GameConfig.c_MaxDifficultyIncrease;
            }
            if (Global.debugMode)
                print(rule.GetDifficultyActionName() + ": " + difficultyChange);
            Global.difficultyLevel += difficultyChange;
        }
        Global.difficultyLevel = Mathf.Clamp(Global.difficultyLevel, GameConfig.minDifficultyLevel, GameConfig.maxDifficultyLevel);
        if (Global.debugMode)
            Utility.PrintCol("NEW DIFFICULTY: " + Global.difficultyLevel, "00FF00");
    }
}
