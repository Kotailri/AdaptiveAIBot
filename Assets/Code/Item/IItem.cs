public interface IItem
{
    public string GetItemName();
    public ItemType GetItemType();
    public void OnInventoryAdd(string recieverTag);
}
