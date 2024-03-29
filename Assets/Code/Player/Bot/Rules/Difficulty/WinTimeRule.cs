using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTimeRule : MonoBehaviour, IDifficultyRule
{
    private float current_timer = 0.0f;
    private List<float> playerWinTimes = new List<float>();
    private List<float> botWinTimes = new List<float>();

    private void Update()
    {
        current_timer += Time.deltaTime;
    }

    public DifficultyRule GetDifficultyActionName()
    {
        return DifficultyRule.WinTime;
    }

    public float GetDifficultyLevelChange(PlayerType winner)
    {
        if (winner == PlayerType.Player)
        {
            playerWinTimes.Add(current_timer);
            current_timer = 0.0f;
        }

        if (winner == PlayerType.Bot)
        {
            botWinTimes.Add(current_timer);
            current_timer = 0.0f;
        }

        if (playerWinTimes.Count >= GameConfig.c_WinTimeRoundCount || botWinTimes.Count >= GameConfig.c_WinTimeRoundCount)
        {
            float averagePlayerTime = Math.AverageFloat(playerWinTimes);
            float averageBotTime = Math.AverageFloat(botWinTimes);
            float averageTimeDifference = Mathf.Abs(Mathf.Abs(averageBotTime) - Mathf.Abs(averagePlayerTime));

            if (Global.debugMode)
                Utility.PrintCol("TIME DIFF AVG: " + averageTimeDifference, "00FF00");

            playerWinTimes.Clear();
            botWinTimes.Clear();

            if (averagePlayerTime < averageBotTime && winner == PlayerType.Player)
            {
                // increase diff
                return (Mathf.Abs(averageTimeDifference)) * GameConfig.c_WinTimeDifficultyScaling * GameConfig.c_GlobalDifficultyScaling;
            }
            else if (winner == PlayerType.Bot)
            {
                // decrease diff
                return -(Mathf.Abs(averageTimeDifference)) * GameConfig.c_WinTimeDifficultyScaling * GameConfig.c_GlobalDifficultyScaling;
            }
        }

        return 0.0f;
    }
}
