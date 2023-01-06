using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static float c_PlayerMovespeed = 5.0f;
    public static float c_PlayerShootCooldown = 0.75f;
    public static float c_ProjectileSpeed = 15.0f;
    public static float c_MaxProjectileDistance = 15.0f;

    public enum ControlType { WASD, ARROWS}

}
