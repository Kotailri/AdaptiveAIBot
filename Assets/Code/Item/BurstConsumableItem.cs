using UnityEngine;

public class BurstConsumableItem : MonoBehaviour, IItem, IResettable
{
    private ItemName itemName = ItemName.BurstConsumable;

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
        return ItemType.Consumable;
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
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
