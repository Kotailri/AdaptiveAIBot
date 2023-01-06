using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    private float DistanceTravelled = 0;
    private Vector2 SpawnLocation;

    public float MaxDist;

    private void OnEnable()
    {
        SpawnLocation = transform.position;
        //GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        DistanceTravelled = Vector2.Distance(transform.position, SpawnLocation);
        if (DistanceTravelled > GameConfig.c_MaxProjectileDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DistanceTravelled <= MaxDist)
            return;

        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
