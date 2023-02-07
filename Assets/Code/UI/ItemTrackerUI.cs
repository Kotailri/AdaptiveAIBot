using TMPro;
using UnityEngine;

public class ItemTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI playerPoisonText;
    public TextMeshProUGUI playerBurstText;

    [Space(5.0f)]

    public TextMeshProUGUI botPoisonText;
    public TextMeshProUGUI botBurstText;

    public void Awake()
    {
        Global.itemTrackerUI = this;
        UpdatePlayerItemUI(0, 0);
        UpdateBotItemUI(0, 0);
    }

    public void UpdatePlayerItemUI(int playerPoisonCount, int playerBurstCount)
    {
        playerPoisonText.text = "x" + playerPoisonCount;
        playerBurstText.text = "x" + playerBurstCount;
    }

    public void UpdateBotItemUI(int botPoisonCount, int botBurstCount)
    {
        botPoisonText.text = "x" + botPoisonCount;
        botBurstText.text = "x" + botBurstCount;
    }
}
