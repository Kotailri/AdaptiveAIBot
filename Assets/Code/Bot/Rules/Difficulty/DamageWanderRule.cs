using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWanderRule : MonoBehaviour, IDifficultyRule
{
    public DifficultyRule GetDifficultyActionName()
    {
        return DifficultyRule.DamageWander;
    }

    public float GetDifficultyLevelChange(PlayerType winner)
    {
        float difficulty = Global.playertracker.PlayerHitsDuringWander 
            * GameConfig.c_DamageWanderDifficultyScaling 
            * GameConfig.c_GlobalDifficultyScaling;
        Global.playertracker.PlayerHitsDuringWander = 0;
        return difficulty;
    }
}
