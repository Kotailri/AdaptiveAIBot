using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTrackerUI : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI playerPoisonText;
    public TextMeshProUGUI playerBurstText;

    [Space(5.0f)]

    public TextMeshProUGUI botPoisonText;
    public TextMeshProUGUI botBurstText;

    [Space(15.0f)]
    [Header("Icons")]
    public Image playerPoisonImg;
    public Image playerBurstImg;

    [Space(5.0f)]
    public Image botPoisonImg;
    public Image botBurstImg;

    public void Awake()
    {
        Global.itemTrackerUI = this;
        UpdatePlayerItemUI(0, 0);
        UpdateBotItemUI(0, 0);
    }

    public void UpdatePlayerItemUI(int playerPoisonCount, int playerBurstCount)
    {
        if (playerPoisonCount == 0)
            playerPoisonImg.color = new Color(1,1,1,0.25f);
        else
            playerPoisonImg.color = new Color(1, 1, 1, 1);

        if (playerBurstCount == 0)
            playerBurstImg.color = new Color(1, 1, 1, 0.25f);
        else
            playerBurstImg.color = new Color(1, 1, 1, 1);

        playerPoisonText.text = "x" + playerPoisonCount;
        playerBurstText.text = "x" + playerBurstCount;
    }

    public void UpdateBotItemUI(int botPoisonCount, int botBurstCount)
    {
        if (botPoisonCount == 0)
            botPoisonImg.color = new Color(1, 1, 1, 0.25f);
        else
            botPoisonImg.color = new Color(1, 1, 1, 1);

        if (botBurstCount == 0)
            botBurstImg.color = new Color(1, 1, 1, 0.25f);
        else
            botBurstImg.color = new Color(1, 1, 1, 1);

        botPoisonText.text = "x" + botPoisonCount;
        botBurstText.text = "x" + botBurstCount;
    }
}
