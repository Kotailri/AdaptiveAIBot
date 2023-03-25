using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShoot : MonoBehaviour, IActionHasActionCheck, IActionHasUpdateAction, IActionHasActionChance, IActionHasCleanup, IActionExcludeState
{
    [Header("Projectiles")]
    public GameObject projectile;
    public GameObject projectile_big;

    [Header("Aim")]
    private LineRenderer lineRenderer;
    public LayerMask AimTargets;
    public float radius;

    [Header("Shoot")]
    public float CurrentShootTimer = GameConfig.c_PlayerShootCooldown;
    public float CurrentShootTimer_big = GameConfig.c_PlayerShootCooldown_big;

    public bool canShoot = true;
    public bool canShoot_big = true;

    [Header("Leading Shots")]
    public GameObject player;
    public float leadAmount = 0.25f;

    private float currentCounterAttackTimer = 0.0f;
    [HideInInspector]
    public bool detectHits = false;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private bool Aim(bool enhancedAim = false)
    {
        Vector2 dir = (player.transform.position - transform.position).normalized;

        if (enhancedAim && Vector2.Distance(player.transform.position, transform.position) < 5.0f)
        {
            Vector3 velocity = player.GetComponent<Rigidbody2D>().velocity * 0.5f;
            dir = (player.transform.position + velocity - transform.position).normalized;
        }
        
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotation), (enhancedAim ? 10.0f : 5.0f)  * Time.deltaTime * 10);

        Vector2 origin = transform.position;
        Vector2 direction = (player.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, Mathf.Infinity, AimTargets);
        if (hit.collider == null)
        {
            return false;
        }

        if (hit.collider.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    private void ShootSmall()
    {
        float timerDecrease = Mathf.Clamp(Global.difficultyLevel / 10, 0, GameConfig.c_PlayerShootCooldown / 3);
        if (CurrentShootTimer <= (GameConfig.c_PlayerShootCooldown - timerDecrease))
        {
            return;
        }
        CurrentShootTimer = 0;
        DetectCounterAttacks();
        float angleVariance = Mathf.Clamp(Global.difficultyLevel * 5, -40.0f, 0.0f);
        float randomAngle = Random.Range(-angleVariance, angleVariance);

        transform.rotation *= Quaternion.Euler(0f, 0f, randomAngle);

        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        AudioManager.instance.PlaySound("boom");
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
        proj.GetComponent<BulletCollision>().damageBoost = Global.botDamageBoost;

        if (Global.playerAttackCounterLevel >= Random.Range(1,8))
        {
            GetComponent<StateManager>().ForceState(ActionState.Flee);
        }
    }

    private void ShootBig()
    {
        if (CurrentShootTimer_big <= (GameConfig.c_PlayerShootCooldown_big - Mathf.Clamp(Global.difficultyLevel / 10, 0, GameConfig.c_PlayerShootCooldown_big / 2)))
        {
            return;
        }
        CurrentShootTimer_big = 0;
        DetectCounterAttacks();
        StartCoroutine(WindUpShoot());
    }

    private IEnumerator WindUpShoot()
    {
        GetComponent<BotMove>().ToggleCanMove(false);
        GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;

        yield return new WaitForSecondsRealtime(GameConfig.c_WindupDelay);

        GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1,0.36f, 0.315f, 1);
        GetComponent<BotMove>().ToggleCanMove(true);

        float angleVariance = Mathf.Clamp(Global.difficultyLevel * 5, -40.0f, 0.0f);
        float randomAngle = Random.Range(-angleVariance, angleVariance);

        transform.rotation *= Quaternion.Euler(0f, 0f, randomAngle);

        GameObject proj = Instantiate(projectile_big, transform.position, transform.rotation);
        AudioManager.instance.PlaySound("boom");
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
        proj.GetComponent<BulletCollision>().isBig = true;
        proj.GetComponent<BulletCollision>().damageBoost = Global.botDamageBoost_big;
    }

    public void DetectCounterAttacks()
    {
        detectHits = true;
        currentCounterAttackTimer = 0;
    }

    private void Update()
    {
        CurrentShootTimer += Time.deltaTime;
        CurrentShootTimer_big += Time.deltaTime;

        if (detectHits)
        {
            currentCounterAttackTimer += Time.deltaTime;
            if (currentCounterAttackTimer >= GameConfig.c_CounterAttackTime)
                detectHits = false;
        }
        
    }

    public bool CheckAction()
    {
        if (Global.difficultyLevel > Random.Range(3.0f, 8.0f))
        {
            return Aim(enhancedAim: true);
        }
        else
        {
            return Aim();
        }
            
    }

    public void ExecuteAction()
    {
        float shootChance = Random.Range(0f, 8f);
        if (Global.difficultyLevel > shootChance && Global.playerAttackCounterLevel >= Random.Range(0f, 7f))
        {
            if (Global.playertracker && Global.playertracker.playerStopped)
            {
                ShootBig();
                Invoke(nameof(ShootSmall), 1.0f);
            }
            else
            {
                ShootSmall();
            }
        }
        else
        {
            if (shootChance > 1.5f)
                ShootSmall();
            else
                ShootBig();
        }
    }

    public float GetActionChance()
    {
        return Mathf.Clamp(Global.difficultyLevel / 8.0f, 0.03f, 1.0f);
    }

    public void Cleanup()
    {
        if (lineRenderer)
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
        
    }

    public List<ActionState> GetExcludedActionStates()
    {
        return new List<ActionState>() { ActionState.Wander };
    }
}
