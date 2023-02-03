using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject projectile_big;
    public PlayerGunRotation GunRotation;

    private float CurrentShootTimer = GameConfig.c_PlayerShootCooldown;
    private float CurrentShootTimer_big = GameConfig.c_PlayerShootCooldown_big;

    private void Update()
    {
        CurrentShootTimer += Time.deltaTime;
        CurrentShootTimer_big += Time.deltaTime;

        if (Input.GetMouseButton(0) && CurrentShootTimer >= GameConfig.c_PlayerShootCooldown)
        {
            CurrentShootTimer = 0;
            GameObject proj = Instantiate(projectile, transform.position, GunRotation.GetAngleFacing());
            proj.GetComponent<BulletCollision>().playerType = PlayerType.Player;
            proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
            proj.GetComponent<BulletCollision>().damageBoost = Global.playerDamageBoost;
        }

        if (Input.GetMouseButton(1) && CurrentShootTimer_big >= GameConfig.c_PlayerShootCooldown_big)
        {
            CurrentShootTimer_big = 0;
            StartCoroutine(WindUpShoot());
        }
    }

    private IEnumerator WindUpShoot()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<PlayerMovement>().EnableMovement(false);

        yield return new WaitForSecondsRealtime(GameConfig.c_WindupDelay);

        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<PlayerMovement>().EnableMovement(true);

        GameObject proj = Instantiate(projectile_big, transform.position, GunRotation.GetAngleFacing());
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Player;
        proj.GetComponent<BulletCollision>().isBig = true;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed_big;
        proj.GetComponent<BulletCollision>().damageBoost = Global.playerDamageBoost_big;
    }
}
