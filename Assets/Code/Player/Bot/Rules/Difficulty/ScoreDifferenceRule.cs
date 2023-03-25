using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScoreDifferenceRule : MonoBehaviour, IDifficultyRule
{
    private float prevDifficulty = 0.0f;

    public DifficultyRule GetDifficultyActionName()
    {
        return DifficultyRule.ScoreDifference;
    }

    public float GetDifficultyLevelChange(PlayerType winner)
    {
        float difficulty = GetScoreDifference() * GameConfig.c_GlobalDifficultyScaling;
        if (difficulty == 0)
        {
            prevDifficulty = difficulty;
            return difficulty;
        }

        float newDifficulty = difficulty - prevDifficulty;
        prevDifficulty = difficulty;
        return newDifficulty;
    }

    private float GetScoreDifference()
    {
        GameManager gm = Global.gamemanager;
        float winRate = gm.PlayerScore - gm.BotScore;

        if ((gm.PlayerScore + gm.BotScore) < GameConfig.c_ScoreDifferenceRoundCount)
            return 0.0f;

        return (float)winRate * GameConfig.c_WinRateDifficultyScaling;
    }
}
