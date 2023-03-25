using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IResettable
{
    private Dictionary<ItemName, int> consumables = new Dictionary<ItemName, int>();
    private Dictionary<ItemName, int> statBoosts = new Dictionary<ItemName, int>();

    public void Awake()
    {
        consumables.Add(ItemName.PoisonConsumable, 0);
        consumables.Add(ItemName.BurstConsumable, 0);

        statBoosts.Add(ItemName.SpeedStat, 0);
        statBoosts.Add(ItemName.DamageStat, 0);

        statBoosts.Add(ItemName.HealStat, 0);

        UpdateInventoryUI();
    }

    public int GetItemCount()
    {
        return consumables.Count + statBoosts.Count;
    }

    public int GetConsumableCount()
    {
        return consumables.Count;
    }

    public int GetStatBoostCount()
    {
        return statBoosts.Count;
    }

    void OnDestroy()
    {
        OnDestroyAction();
    }

    /// <summary>
    /// Returns true if item is in the inventory.
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public bool HasItem(ItemName itemName)
    {
        if (consumables.ContainsKey(itemName))
        {
            return consumables[itemName] > 0;
        }

        if (statBoosts.ContainsKey(itemName))
        {
            return statBoosts[itemName] > 0;
        }

        return false;
    }

    /// <summary>
    /// Checks if item is in inventory and reduces its count by 1. Returns true on success.
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public bool ConsumeItem(ItemName itemName)
    {
        if (consumables[itemName] > 0)
        {
            consumables[itemName]--;
            UpdateInventoryUI();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds item to inventory by ItemName.
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="amount"></param>
    public void AddItems(ItemName itemName, int amount)
    {
        consumables[itemName] += amount;
        UpdateInventoryUI();
    }

    /// <summary>
    /// Adds item to inventory by IItem object.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="recieverTag"></param>
    public void AddItem(IItem item, string recieverTag)
    {
        if (item is IOnInventoryAddEffect onInvAdd)
            onInvAdd.OnInventoryAdd(recieverTag);

        if (item.GetItemType() == ItemType.Consumable)
        {
            consumables[item.GetItemName()]++;
        }

        if (item.GetItemType() == ItemType.StatBoost)
        {
            statBoosts[item.GetItemName()]++;
        }

        UpdateInventoryUI();
    }

    /// <summary>
    /// Updates the UI that displays player inventory.
    /// </summary>
    private void UpdateInventoryUI()
    {
        if (gameObject.tag == "Player")
            Global.statTrackerUI.UpdatePlayerStatUI(statBoosts[ItemName.DamageStat], statBoosts[ItemName.SpeedStat]);
        if (gameObject.tag == "Bot")
            Global.statTrackerUI.UpdateBotStatUI(statBoosts[ItemName.DamageStat], statBoosts[ItemName.SpeedStat]);

        if (gameObject.tag == "Player")
            Global.itemTrackerUI.UpdatePlayerItemUI(consumables[ItemName.PoisonConsumable], consumables[ItemName.BurstConsumable]);
        if (gameObject.tag == "Bot")
            Global.itemTrackerUI.UpdateBotItemUI(consumables[ItemName.PoisonConsumable], consumables[ItemName.BurstConsumable]);
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void ResetObject()
    {
        consumables[ItemName.PoisonConsumable] = 0;
        consumables[ItemName.BurstConsumable] = 0;

        statBoosts[ItemName.SpeedStat] = 0;
        statBoosts[ItemName.DamageStat] = 0;

        Global.playerSpeedBoost = 0.0f;
        Global.botSpeedBoost = 0.0f;

        Global.playerDamageBoost = 0;
        Global.playerDamageBoost_big = 0;
        Global.botDamageBoost = 0;
        Global.botDamageBoost_big = 0;

        UpdateInventoryUI();
    }

    void Start()
    {
        InitResettable();   
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }
}
