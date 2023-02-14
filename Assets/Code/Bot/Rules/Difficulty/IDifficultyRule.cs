public enum DifficultyRule
{
    WinRate,
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

