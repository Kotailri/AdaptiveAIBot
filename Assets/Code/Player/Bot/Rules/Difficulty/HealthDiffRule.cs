using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDiffRule : MonoBehaviour, IDifficultyRule
{
    public DifficultyRule GetDifficultyActionName()
    {
        return DifficultyRule.HealthDiff;
    }

    public float GetDifficultyLevelChange(PlayerType winner)
    {
        GameManager pt = Global.gamemanager;
        if (winner == PlayerType.Player)
        {
            int healthDiff = pt.PlayerHealth.health;
            return healthDiff * GameConfig.c_HealthDiffDifficultyScaling * GameConfig.c_GlobalDifficultyScaling;
        }
        else
        {
            int healthDiff = pt.BotHealth.health;
            return -healthDiff * GameConfig.c_HealthDiffDifficultyScaling * GameConfig.c_GlobalDifficultyScaling;
        }

    }
}
