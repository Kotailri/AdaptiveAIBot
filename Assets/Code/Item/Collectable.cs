using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bot"))
        {
            collision.GetComponent<Inventory>().AddItem(GetComponent<IItem>(), collision.tag);
            Destroy(gameObject);
        }
    }
}
