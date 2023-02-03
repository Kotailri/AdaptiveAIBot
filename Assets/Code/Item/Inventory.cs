using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IResettable
{
    private Dictionary<string, int> consumables = new Dictionary<string, int>();
    private Dictionary<string, int> statBoosts = new Dictionary<string, int>();

    public void Awake()
    {
        consumables.Add("poison_consumable", 0);
        consumables.Add("shield_consumable", 0);

        statBoosts.Add("speed_stat", 0);
        statBoosts.Add("damage_stat", 0);
    }

    void OnDestroy()
    {
        OnDestroyAction();
    }

    public void AddItem(IItem item, string recieverTag)
    {
        item.OnInventoryAdd(recieverTag);

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
            Global.statTrackerUI.UpdatePlayerStatUI(statBoosts["damage_stat"], statBoosts["speed_stat"]);
        if (gameObject.tag == "Bot")
            Global.statTrackerUI.UpdateBotStatUI(statBoosts["damage_stat"], statBoosts["speed_stat"]);
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void ResetObject()
    {
        consumables["poison_consumable"] = 0;
        consumables["shield_consumable"] = 0;

        statBoosts["speed_stat"] = 0;
        statBoosts["damage_stat"] = 0;

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
