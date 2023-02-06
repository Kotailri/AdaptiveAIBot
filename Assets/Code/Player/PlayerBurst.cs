using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurst : MonoBehaviour
{
    public GameObject burst;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject b = Instantiate(burst, transform.position, Quaternion.identity);
            b.GetComponent<Burst>().SetFollow(gameObject);
        }
    }
}
