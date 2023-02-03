using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BotShoot : MonoBehaviour, IAction
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

    [Header("Debug")]
    public bool debug = true;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private bool Aim()
    {
        Vector2 origin = transform.position;
        Vector2 direction = -transform.up;
        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, Mathf.Infinity, AimTargets);
        if (debug) lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y, -0.1f));
        if (hit.collider == null)
        {
            if (debug) lineRenderer.SetPosition(1, transform.position + (Vector3)direction * Mathf.Infinity);
            return false;
        }

        if (debug) lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -0.1f));

        if (hit.collider.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    private void ShootSmall()
    {
        if (CurrentShootTimer < GameConfig.c_PlayerShootCooldown)
        {
            return;
        }
        CurrentShootTimer = 0;

        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
        proj.GetComponent<BulletCollision>().damageBoost = Global.botDamageBoost;
    }

    private void ShootBig()
    {
        if (CurrentShootTimer_big < GameConfig.c_PlayerShootCooldown_big)
        {
            return;
        }
        CurrentShootTimer_big = 0;

        StartCoroutine(WindUpShoot());
    }

    private IEnumerator WindUpShoot()
    {
        GetComponent<BotMove>().ToggleCanMove(false);
        GetComponent<SpriteRenderer>().color = UnityEngine.Color.black;

        yield return new WaitForSecondsRealtime(GameConfig.c_WindupDelay);

        GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1,0.36f, 0.315f, 1);
        GetComponent<BotMove>().ToggleCanMove(true);

        GameObject proj = Instantiate(projectile_big, transform.position, transform.rotation);
        proj.GetComponent<BulletCollision>().playerType = PlayerType.Bot;
        proj.GetComponent<Rigidbody2D>().velocity = (proj.transform.up).normalized * -GameConfig.c_ProjectileSpeed;
        proj.GetComponent<BulletCollision>().isBig = true;
        proj.GetComponent<BulletCollision>().damageBoost = Global.botDamageBoost_big;
    }

    private void Update()
    {
        CurrentShootTimer += Time.deltaTime;
        CurrentShootTimer_big += Time.deltaTime;
    }

    bool IAction.CheckAction()
    {
        return Aim();
    }

    void IAction.ExecuteAction()
    {
        // compare distances, decide small or big shoot
        float shootChance = Random.Range(0f, 1f);
        if (shootChance > 0.3f)
            ShootSmall();
        else
            ShootBig();
    }

    float IAction.GetActionChance()
    {
        // always shoot
        return 1;
    }

    public ActionState GetActionState()
    {
        return ActionState.Attack;
    }

    public void Cleanup()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }
}
