using UnityEngine;

public class CreateProjectileObstacle : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject obstacle2;
    public LayerMask botLayer;

    public GameObject SpawnObstacle(bool hasNavMesh = true)
    {
        if (!hasNavMesh)
        {
            Vector2 direction = GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject rectangle = Instantiate(obstacle2, transform.position, rotation);
            return rectangle;
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.4f, GetComponent<Rigidbody2D>().velocity.normalized, Mathf.Infinity, botLayer);
        if (hit.collider == null)
        {
            Vector2 direction = GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject rectangle = Instantiate(obstacle, transform.position, rotation);
            return rectangle;
        }
        return null;
    }
}
