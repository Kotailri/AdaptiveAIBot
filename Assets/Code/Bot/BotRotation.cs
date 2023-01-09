using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRotation : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 5.0f;

    void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotation), rotationSpeed * Time.deltaTime);
    }
}
