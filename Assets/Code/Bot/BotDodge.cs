using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.UI.Image;

public class BotDodge : MonoBehaviour
{
    private float scanDistance = 8.0f;
    private float dodgeRadius = 0.5f;
    private float dodgeDistance = 2.0f;
    private float dodgeSpeed = 15.0f;

    private List<GameObject> incomingBullets = new List<GameObject> ();
    public LayerMask bulletLayer;
    public LayerMask dodgeLayer;

    private BotAreaScanner scanner;
    private Rigidbody2D RB;

    [HideInInspector]
    public bool isDodging = false;

    private void Awake()
    {
        scanner = GetComponent<BotAreaScanner>();
        RB = GetComponent<Rigidbody2D>();
    }

    public void Dodge()
    {
        bool dodgeChance = Global.difficultyLevel >= Random.Range(0.0f, 5.0f);
        if (!dodgeChance)
        {
            return;
        }

        if (!GetIncomingBullets() || !isDodging)
        {
            return;
        }

        // Modify difficulty
        scanDistance = Mathf.Clamp(Global.difficultyLevel + 1.0f, 2.0f, 8.0f);
        dodgeSpeed = Mathf.Clamp(Global.difficultyLevel + 2.5f, 1.0f, 20.0f);

        Vector2 toTarget = GetNearestBullet().transform.position - transform.position;
        Vector2 perpendicular = Vector2.Perpendicular(toTarget);

        RaycastHit2D hit;
        RB.velocity = Vector2.zero;

        float dot = Vector3.Dot(perpendicular, toTarget);
        if (dot < 0)
        {
            perpendicular = -perpendicular;
        }

        // move perpendicular
        hit = Physics2D.CircleCast(transform.position, dodgeRadius, perpendicular, dodgeDistance, dodgeLayer);
        if (hit.collider == null)
        {
            RB.velocity = perpendicular.normalized * dodgeSpeed;
            
        }
        // move back
        else
        {
            RB.velocity = -toTarget.normalized * dodgeSpeed;
        }
    }

    private void Update()
    {
        GetIncomingBullets();
        Dodge();
    }

    public GameObject GetNearestBullet()
    {
        if (incomingBullets.Count > 0)
        {
            return incomingBullets[0];
        }
        return null;
    }

    private bool GetIncomingBullets()
    {
        incomingBullets.Clear ();
        foreach (GameObject bullet in scanner.GetScannedPlayerBullets(scanDistance))
        {
            if (!bullet.activeSelf)
                continue;

            Vector2 direction = bullet.GetComponent<Rigidbody2D>().velocity.normalized;
            Vector2 origin = bullet.transform.position;

            RaycastHit2D hit = Physics2D.CircleCast(origin, 0.4f, direction, Mathf.Infinity, bulletLayer);

            if (hit.collider == null)
            {
                continue;
            }

            if (hit.collider.CompareTag("Bot"))
            {
                incomingBullets.Add(bullet);
            }
        }

        incomingBullets.Sort((a, b) => // Sort by distance
        {
            float distanceToA = Vector2.Distance(transform.position, a.transform.position);
            float distanceToB = Vector2.Distance(transform.position, b.transform.position);
            return distanceToA.CompareTo(distanceToB);
        });

        return incomingBullets.Count > 0;
             
    }
}
