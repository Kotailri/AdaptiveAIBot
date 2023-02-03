using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatTrackerUI : MonoBehaviour
{
    public TextMeshProUGUI playerDamageText;
    public TextMeshProUGUI playerSpeedText;

    [Space(5.0f)]

    public TextMeshProUGUI botDamageText;
    public TextMeshProUGUI botSpeedTextText;

    public void Awake()
    {
        Global.statTrackerUI = this;
        UpdatePlayerStatUI(0, 0);
        UpdateBotStatUI(0, 0);
    }

    public void UpdatePlayerStatUI(int playerDamageLv, int playerSpeedLv)
    {
        playerDamageText.text = "x" + playerDamageLv;
        playerSpeedText.text = "x" + playerSpeedLv;
    }

    public void UpdateBotStatUI(int botDamageLv, int botSpeedLv)
    {
        botSpeedTextText.text = "x" + botSpeedLv;
        botDamageText.text = "x" + botDamageLv;
    }
}
