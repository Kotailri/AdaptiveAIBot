using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static float c_PlayerMovespeed = 6.0f;
    public static float c_BotMovespeed = 4.0f;

    #region Shooting
    // Cooldown
    public static float c_PlayerShootCooldown = 0.75f;
    public static float c_PlayerShootCooldown_big = 2.0f;

    // Speed
    public static float c_ProjectileSpeed = 15.0f;
    public static float c_ProjectileSpeed_big = 10.0f;

    public static float c_WindupDelay = 0.35f;

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
    public static float c_ItemSpawnTime = 5.0f;
    public static int c_MaxItemCount = 3;
}
