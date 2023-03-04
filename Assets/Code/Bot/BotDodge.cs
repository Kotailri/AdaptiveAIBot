using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BotDodge : MonoBehaviour
{
    public float dodgeDistance;

    private List<GameObject> incomingBullets = new List<GameObject> ();
    public LayerMask bulletLayer;

    private BotAreaScanner scanner;

    private void Awake()
    {
        scanner = GetComponent<BotAreaScanner>();
    }

    private void Start()
    {
        //StartCoroutine(UpdateScan());
    }

    private IEnumerator UpdateScan() 
    {
        GetIncomingBullets();
        yield return new WaitForSecondsRealtime(1.0f);
        StartCoroutine(UpdateScan());
    }

    private void FixedUpdate()
    {
        if (!GetIncomingBullets())
        {
            return;
        }

        // Calculate the vector from the current object to the approaching object
        Vector2 toTarget = GetNearestBullet().transform.position - transform.position;

        // Calculate a vector that is perpendicular to the toTarget vector
        Vector2 perpendicular = Vector2.Perpendicular(toTarget);

        // Use the dot product to determine the direction of the perpendicular vector
        float dot = Vector2.Dot(perpendicular, toTarget);
        if (dot < 0)
        {
            perpendicular = -perpendicular;
        }

        // The perpendicular vector is now perpendicular to the toTarget vector and points in the direction of the perpendicular approach
        UnityEngine.Debug.DrawRay(transform.position, perpendicular, Color.green);
        Time.timeScale = 0;
    }

    private GameObject GetNearestBullet()
    {
        return incomingBullets[0];
    }

    private bool GetIncomingBullets()
    {
        incomingBullets.Clear ();
        foreach (GameObject bullet in scanner.GetScannedPlayerBullets(dodgeDistance))
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
