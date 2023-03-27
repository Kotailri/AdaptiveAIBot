using UnityEngine;

public class AccuracyDiffRule : MonoBehaviour, IDifficultyRule
{
    public DifficultyRule GetDifficultyActionName()
    {
        return DifficultyRule.AccuracityDiff;
    }

    public float GetDifficultyLevelChange(PlayerType winner)
    {
        PlayerTracker tracker = Global.playertracker;
        float playerAccuracy = (float)tracker.PlayerHitsLanded / (float)(tracker.PlayerHitsLanded + tracker.PlayerHitsMissed);
        float botAccuracy = (float)tracker.BotHitsLanded / (float)(tracker.BotHitsLanded + tracker.BotHitsMissed);

        tracker.PlayerHitsLanded = 0;
        tracker.BotHitsLanded = 0;
        tracker.PlayerHitsMissed = 0;
        tracker.BotHitsMissed = 0;

        if (float.IsNaN(playerAccuracy) || float.IsNaN(botAccuracy))
            return 0.0f;

        return (playerAccuracy - botAccuracy) * GameConfig.c_AccracyDiffDifficultyScaling * GameConfig.c_GlobalDifficultyScaling;
    }
}
