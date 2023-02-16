public enum DifficultyRule
{
    ScoreDifference,
    WinTime,
    HealthDiff,
    AccuracityDiff,
    DamageWander
}

public interface IDifficultyRule
{
    public DifficultyRule GetDifficultyActionName();
    public float GetDifficultyLevelChange(PlayerType winner);
}

