using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static float c_PlayerMovespeed = 8.0f;

    #region Shooting
    // Cooldown
    public static float c_PlayerShootCooldown = 0.75f;
    public static float c_PlayerShootCooldown_big = 2.0f;

    // Speed
    public static float c_ProjectileSpeed = 15.0f;
    public static float c_ProjectileSpeed_big = 10.0f;

    public static float c_WindupDelay = 0.35f;

    public static int c_BulletDamage = 15;
    public static int c_BulletDamage_big = 25;

    // Distance
    public static float c_MaxProjectileDistance = 250.0f;
    #endregion

    public static float c_DistanceTrackingTimer = 1.0f;

    public enum ControlType { WASD, ARROWS}

}
