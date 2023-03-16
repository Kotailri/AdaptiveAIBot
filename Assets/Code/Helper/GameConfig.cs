using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static float c_PlayerMovespeed = 5.0f;
    public static float c_BotMovespeed = 3.0f;

    #region Shooting
    // Cooldown
    public static float c_PlayerShootCooldown = 0.75f;
    public static float c_PlayerShootCooldown_big = 2.0f;

    // Speed
    public static float c_ProjectileSpeed = 15.0f;
    public static float c_ProjectileSpeed_big = 10.0f;

    public static float c_WindupDelay = 0.35f;

    // Damage
    public static int c_BulletDamage = 15;
    public static int c_BulletDamage_big = 35;

    // Distance
    public static float c_MaxProjectileDistance = 250.0f;
    #endregion

    public static float c_DistanceTrackingTimer = 1.0f;

    // Spawn Position
    public static Vector3 c_PlayerSpawnPosition = new Vector3(-11.9899998f, -5.63999987f, 0);
    public static Vector3 c_BotSpawnPosition = new Vector3(11.5200005f, 3.333f, 0);
    public static Bounds c_WorldBounds = new Bounds(new Vector2(-12.4f, -6.4f), new Vector2(12.4f, 4.5f));

    // Items
    public static float c_ItemSpawnTime = 1.0f;
    public static int c_MaxItemCount = 6;

    // Burst Shield
    public static KeyCode c_BurstKeyCode = KeyCode.E;
    public static int c_BurstDamage = 30;
    public static float c_BurstMaxSize = 4.0f;
    public static float c_BurstGrowSpeedMultiplier = 18.0f;
    public static float c_BurstCooldown = 2.5f;

    // Poison
    public static KeyCode c_PoisonKeyCode = KeyCode.Q;
    public static float c_PoisonTimer = 0.25f;
    public static int c_PoisonTickDamage = 5;
    public static int c_PoisonEnterDamage = 10;
    public static float c_PoisonExpireTime = 15.0f;

    // Difficulty Scaling Speed
    public static float c_GlobalDifficultyScaling = 1.0f;
    public static float c_MaxDifficultyIncrease = 1.0f;

    public static float c_WinRateDifficultyScaling = 0.2f;
    public static float c_WinTimeDifficultyScaling = 0.1f;
    public static float c_HealthDiffDifficultyScaling = 0.001f;
    public static float c_DamageWanderDifficultyScaling = 0.3f;
    public static float c_AccracyDiffDifficultyScaling = 1.0f;

    // Difficulty Rule
    public static float maxDifficultyLevel = 10.0f;
    public static float minDifficultyLevel = -10.0f;

    public static int c_WinTimeRoundCount = 2;
    public static int c_ScoreDifferenceRoundCount = 2;

    public static float c_OvertimeTime = 30.0f;
    public static int c_OvertimeDamage = 4;

    // Playstyle Rule
    public static int maxPlaystyleLevel = 10;
    public static int minPlaystyleLevel = 0;
    
    public static float c_AggroApproachDist = 5f;
    public static float c_CounterAttackTime = 1f;

}
