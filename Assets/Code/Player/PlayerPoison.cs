using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoison : MonoBehaviour
{
    public GameObject poison;
    private Inventory inv;

    private void Awake()
    {
        inv = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(GameConfig.c_PoisonKeyCode))
        {
            if (inv.ConsumeItem(ItemName.PoisonConsumable))
            {
                Instantiate(poison, transform.position, Quaternion.identity);
                Global.playertracker.PlayerItemsUsed++;
            }
        }
    }
}
