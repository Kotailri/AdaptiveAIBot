using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAreaScanner : MonoBehaviour
{
    public List<GameObject> playerBullets = new List<GameObject>();
    private float radius = 4.0f;

    public List<GameObject> GetScannedPlayerBullets()
    {
        return new List<GameObject>(playerBullets);
    }

    private void Update()
    {
        playerBullets.Clear();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Bullet") && collider.gameObject.activeSelf)
            {
                if (collider.gameObject.GetComponent<BulletCollision>().playerType == PlayerType.Player)
                    playerBullets.Add(collider.gameObject);
            }
        }
    }
}
