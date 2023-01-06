using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    public PlayerGunRotation GunRotation;

    private float CurrentShootTimer = GameConfig.c_PlayerShootCooldown;

    private void Update()
    {
        CurrentShootTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && CurrentShootTimer >= GameConfig.c_PlayerShootCooldown)
        {
            CurrentShootTimer = 0;
            GameObject proj = Instantiate(projectile, transform.position, GunRotation.GetAngleFacing());
            proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
        }
    }
}
