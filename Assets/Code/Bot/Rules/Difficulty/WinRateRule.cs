using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WinRateRule : MonoBehaviour, IDifficultyRule
{
    private float prevDifficulty = 0.0f;
    private int minimumRequiredGames = 0;

    public DifficultyRule GetDifficultyActionName()
    {
        return DifficultyRule.WinRate;
    }

    public float GetDifficultyLevelChange(PlayerType winner)
    {
        float difficulty = GetWinRate() * GameConfig.c_GlobalDifficultyScaling;
        if (difficulty == 0)
        {
            prevDifficulty = difficulty;
            return difficulty;
        }

        float newDifficulty = difficulty - prevDifficulty;
        prevDifficulty = difficulty;
        return newDifficulty;
    }

    private float GetWinRate()
    {
        GameManager gm = Global.gamemanager;
        float winRate = gm.PlayerScore - gm.BotScore;

        if ((gm.PlayerScore + gm.BotScore) < minimumRequiredGames)
            return 0.0f;

        return (float)winRate * GameConfig.c_WinRateDifficultyScaling;
    }
}
