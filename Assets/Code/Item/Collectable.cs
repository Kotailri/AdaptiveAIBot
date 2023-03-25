using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Global.playertracker.PlayerItemsCollected++;
        }

        if (collision.CompareTag("Bot"))
        {
            Global.playertracker.BotItemsCollected++;
        }

        if (collision.CompareTag("Player") || collision.CompareTag("Bot"))
        {
            collision.GetComponent<Inventory>().AddItem(GetComponent<IItem>(), collision.tag);
            AudioManager.instance.PlaySound("pop");
            Destroy(gameObject);
        }
    }
}
