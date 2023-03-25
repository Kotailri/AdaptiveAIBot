using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour, IResettable
{
    public GameObject projectile;
    public GameObject projectile_big;
    public PlayerGunRotation GunRotation;

    [Space(5.0f)]
    public ProgressBar cooldownCircle;
    public ProgressBar cooldownCircle_big;

    private float CurrentShootTimer = GameConfig.c_PlayerShootCooldown;
    private float CurrentShootTimer_big = GameConfig.c_PlayerShootCooldown_big;

    private void Start()
    {
        InitResettable();
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }

    public void ResetObject()
    {
        CurrentShootTimer = GameConfig.c_PlayerShootCooldown;
        CurrentShootTimer_big = GameConfig.c_PlayerShootCooldown_big;

        SetCooldownCircles();
    }

    private void SetCooldownCircles()
    {
        float percentCooldown = Mathf.Clamp(GameConfig.c_PlayerShootCooldown - CurrentShootTimer, 0, GameConfig.c_PlayerShootCooldown);
        cooldownCircle.current = (int)(percentCooldown / GameConfig.c_PlayerShootCooldown * 100);

        float percentCooldown_big = Mathf.Clamp(GameConfig.c_PlayerShootCooldown_big - CurrentShootTimer_big, 0, GameConfig.c_PlayerShootCooldown_big);
        cooldownCircle_big.current = (int)(percentCooldown_big / GameConfig.c_PlayerShootCooldown_big * 100);
    }

    private void Update()
    {
        CurrentShootTimer += Time.deltaTime;
        CurrentShootTimer_big += Time.deltaTime;

        SetCooldownCircles();

        if (!GameConfig.c_MouseAimBounds.IsPointInBounds(Camera.main.ScreenToWorldPoint(Input.mousePosition)) && SceneManager.GetActiveScene().name == "GameScene")
        {
            return;
        }

        if (Input.GetMouseButton(0) && CurrentShootTimer >= GameConfig.c_PlayerShootCooldown && Time.timeScale == 1)
        {
            CurrentShootTimer = 0;
            GameObject proj = Instantiate(projectile, transform.position, GunRotation.GetAngleFacing());
            proj.GetComponent<BulletCollision>().playerType = PlayerType.Player;
            if (Time.timeScale == 1) AudioManager.instance.PlaySound("boom");
            proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
            proj.GetComponent<BulletCollision>().damageBoost = Global.playerDamageBoost;
        }

        if (Input.GetMouseButton(1) && CurrentShootTimer_big >= GameConfig.c_PlayerShootCooldown_big && Time.timeScale == 1)
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
        if (Time.timeScale == 1) AudioManager.instance.PlaySound("boom");
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed_big;
        proj.GetComponent<BulletCollision>().damageBoost = Global.playerDamageBoost_big;
    }
}
