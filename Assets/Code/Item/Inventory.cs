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
    }

    void OnDestroy()
    {
        OnDestroyAction();
    }

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

        print(recieverTag + " picked up " + item.GetItemName());
        UpdateInventoryUI();
    }

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
