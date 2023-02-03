using UnityEngine;

public class DamageStatItem : MonoBehaviour, IItem, IResettable
{
    private string itemName = "damage_stat";
    private int damageBoostAmount = 5;
    private int damageBoostAmount_big = 10;

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

    public string GetItemName()
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
            Global.botDamageBoost += damageBoostAmount;
            Global.botDamageBoost_big += damageBoostAmount_big;
        }

        if (recieverTag == "Player")
        {
            Global.playerDamageBoost += damageBoostAmount;
            Global.playerDamageBoost_big += damageBoostAmount_big;
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
