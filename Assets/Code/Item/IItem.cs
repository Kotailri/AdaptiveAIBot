/// <summary>
/// Interface for collectable items.
/// </summary>
public interface IItem
{
    public ItemName GetItemName();
    public ItemType GetItemType();
}

public interface IOnInventoryAddEffect : IItem 
{
    public void OnInventoryAdd(string recieverTag);
}