using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BotBurst : MonoBehaviour, IActionHasUpdateAction
{
    public bool debug;

    [Space(5.0f)]
    public GameObject burst;
    public LayerMask AimTargets;

    private Inventory inv;
    private BotAreaScanner scanner;

    private Timer cooldownTimer;

    private void Awake()
    {
        scanner = GetComponent<BotAreaScanner>();
        inv = GetComponent<Inventory>();
    }

    private void Start()
    {
        cooldownTimer = new Timer(GameConfig.c_BurstCooldown);
    }

    public void CreateBurst()
    {
        if (!cooldownTimer.IsAvailable())
            return;

        cooldownTimer.ResetTimer();
        if (inv.ConsumeItem(ItemName.BurstConsumable))
        {
            GameObject b = Instantiate(burst, transform.position, Quaternion.identity);
            b.GetComponent<Burst>().SetFollow(gameObject);
        }
    }

    public void ExecuteAction()
    {
        if (!CheckDifficulty())
        {
            return;
        }

        if (scanner && CheckPlayerBullets())
        {
            CreateBurst();
        }

        else if (Global.playertracker.CurrentDistance <= 3)
        {
            CreateBurst();
        }
    }
    private bool CheckDifficulty()
    {
        if (Global.difficultyLevel <= 0.0)
        {
            return Global.difficultyLevel <= Random.Range(-10.0f, 0.0f);
        }

        return Global.difficultyLevel >= Random.Range(0.0f, 10.0f);
    }

    private bool CheckPlayerBullets()
    {
        foreach (GameObject bullet in scanner.GetScannedPlayerBullets(4.0f))
        {
            if (!bullet.activeSelf)
                continue;

            Vector2 direction = bullet.GetComponent<Rigidbody2D>().velocity.normalized;
            Vector2 origin = bullet.transform.position;

            RaycastHit2D hit = Physics2D.CircleCast(origin, 0.3f, direction, Mathf.Infinity, AimTargets);

            if (debug) GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, transform.position.y, -0.1f));
            if (hit.collider == null)
            {
                if (debug) GetComponent<LineRenderer>().SetPosition(1, transform.position + (Vector3)direction * Mathf.Infinity);
                continue;
            }

            if (debug) GetComponent<LineRenderer>().SetPosition(1, new Vector3(hit.point.x, hit.point.y, -0.1f));

            if (hit.collider.CompareTag("Bot"))
            {
                return true;
            }
        }
        return false;
    }
}
