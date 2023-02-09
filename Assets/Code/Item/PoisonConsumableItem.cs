using UnityEngine;

public class PoisonConsumableItem : MonoBehaviour, IItem, IResettable
{
    private ItemName itemName = ItemName.PoisonConsumable;

    private void Start()
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
