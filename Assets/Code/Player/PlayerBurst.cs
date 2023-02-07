using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurst : MonoBehaviour
{
    public GameObject burst;
    private Inventory inv;

    private void Awake()
    {
        inv = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inv.ConsumeItem(ItemName.BurstConsumable))
            {
                GameObject b = Instantiate(burst, transform.position, Quaternion.identity);
                b.GetComponent<Burst>().SetFollow(gameObject);
            }
        }
    }
}
