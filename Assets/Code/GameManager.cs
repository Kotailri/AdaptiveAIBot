using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Score")]
    public int PlayerScore = 0;
    public int BotScore = 0;

    [Header("Health")]
    public Health PlayerHealth;
    public Health BotHealth;

    [Space(5.0f)]
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI botScoreText;

    public void RestartGame()
    {
        PlayerScore = 0;
        BotScore = 0;
        ResetObjects();
    }
    
    public void UpdateGM()
    {
        bool BotWin = PlayerHealth.CheckDead();
        bool PlayerWin = BotHealth.CheckDead();

        if (BotWin)
        {
            BotScore++;
            botScoreText.text = BotScore.ToString();
            ResetObjects();
            Global.ruleManager.UpdateDifficulty(PlayerType.Bot);
        }

        if (PlayerWin)
        {
            PlayerScore++;
            playerScoreText.text = PlayerScore.ToString();
            ResetObjects();
            Global.ruleManager.UpdateDifficulty(PlayerType.Player);
        }
    }

    private void ResetObjects()
    {
        foreach (IResettable res in Global.resettables)
        {
            res.ResetObject();
        }
        Invoke("ResetHealth", 0.5f);   
    }

    private void ResetHealth()
    {
        PlayerHealth.ResetObject();
        BotHealth.ResetObject();
    }

    private void Awake()
    {
        Global.gamemanager = this;
    }
}
