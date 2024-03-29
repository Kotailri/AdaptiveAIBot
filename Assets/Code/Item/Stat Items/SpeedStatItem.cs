using UnityEngine;

public class SpeedStatItem : MonoBehaviour, IItem, IResettable, IOnInventoryAddEffect
{
    private ItemName itemName = ItemName.SpeedStat;
    private float speedBoostAmount = 2.0f;

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
            Global.botSpeedBoost += speedBoostAmount;
        }

        if (recieverTag == "Player")
        {
            Global.playerSpeedBoost += speedBoostAmount;
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
