using UnityEngine;

public class HealStatItem : MonoBehaviour, IItem, IResettable, IOnInventoryAddEffect
{
    private ItemName itemName = ItemName.HealStat;
    private int healAmount = 10;

    private void OnEnable()
    {
        InitResettable();
        Global.itemSpawner.currentItems.Add(gameObject);
    }

    void OnDestroy()
    {
        OnDestroyAction();
        Global.itemSpawner.currentItems.Remove(gameObject);
    }

    public ItemName GetItemName()
    {
        return itemName;
    }

    public ItemType GetItemType()
    {
        return ItemType.StatBoost;
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void OnInventoryAdd(string recieverTag)
    {
        if (recieverTag == "Bot")
        {
            Global.gamemanager.BotHealth.UpdateHealth(healAmount);
        }

        if (recieverTag == "Player")
        {
            Global.gamemanager.PlayerHealth.UpdateHealth(healAmount);
        }
    }

    public void ResetObject()
    {
        Destroy(gameObject);
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }
}
