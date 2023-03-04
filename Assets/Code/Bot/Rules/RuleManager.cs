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

    public void UpdateDifficulty(PlayerType winner)
    {
        foreach (IDifficultyRule rule in difficultyRules)
        {
            float difficultyChange = rule.GetDifficultyLevelChange(winner);
            if (difficultyChange > GameConfig.c_MaxDifficultyIncrease)
            {
                difficultyChange = GameConfig.c_MaxDifficultyIncrease;
            }
            print(rule.GetDifficultyActionName() + ": " + difficultyChange);
            Global.difficultyLevel += difficultyChange;
        }
        print("NEW DIFFICULTY: " + Global.difficultyLevel);
    }
}
