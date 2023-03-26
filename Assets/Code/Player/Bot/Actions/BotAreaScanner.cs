using System.Collections.Generic;
using UnityEngine;

public class BotAreaScanner : MonoBehaviour
{
    public List<GameObject> GetScannedPlayerBullets(float radius)
    {
        List<GameObject> playerBullets = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bullet") && collider.gameObject.activeSelf)
            {
                if (collider.gameObject.GetComponent<BulletCollision>().playerType == PlayerType.Player)
                    playerBullets.Add(collider.gameObject);
            }
        }
        return new List<GameObject>(playerBullets);
    }

    public Vector2 LocateNearestItem()
    {
        GameObject closest = null;
        float minDistance = float.MaxValue;

        foreach (GameObject gameObject in Global.itemSpawner.currentItems)
        {
            float distance = Vector2.Distance(gameObject.transform.position, transform.position);
            if (distance < minDistance)
            {
                closest = gameObject;
                minDistance = distance;
            }
        }
        return closest.transform.position;
    }

    public float DistanceToNearestItem()
    {
        return Vector2.Distance(gameObject.transform.position, LocateNearestItem());
    }
}
