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

    [Space(5.0f)]
    public float matchTime = 0.0f;
    private bool overtime = false;

    [Space(5.0f)]
    public Canvas pauseCanvas;

    public void RestartGame()
    {
        PlayerScore = 0;
        BotScore = 0;

        matchTime = 0.0f;
        overtime = false;
        
        ResetObjects();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pauseCanvas.enabled)
        {
            AudioManager.instance.PlaySound("click");
            TogglePause(false);
        }

        if (matchTime < GameConfig.c_OvertimeTime)
        {
            matchTime += Time.deltaTime;
        }
        else if (!overtime)
        {
            overtime = true;
            StartCoroutine(OvertimeDamage());
        }
    }

    private IEnumerator OvertimeDamage()
    {
        PlayerHealth.UpdateHealth(-GameConfig.c_OvertimeDamage);
        BotHealth.UpdateHealth(-GameConfig.c_OvertimeDamage);
        UpdateGM();
        if (overtime)
            yield return new WaitForSeconds(1.0f);
        if (overtime)
            StartCoroutine(OvertimeDamage());
    }

    public void UpdateGM()
    {
        bool BotWin = PlayerHealth.CheckDead();
        bool PlayerWin = BotHealth.CheckDead();

        if (BotWin)
        {
            AudioManager.instance.PlaySound("bad_ding");
            BotScore++;
            botScoreText.text = BotScore.ToString();
            Global.ruleManager.UpdateDifficulty(PlayerType.Bot);

        }

        else if (PlayerWin)
        {
            AudioManager.instance.PlaySound("ding");
            PlayerScore++;
            playerScoreText.text = PlayerScore.ToString();
            Global.ruleManager.UpdateDifficulty(PlayerType.Player);
        }

        if (BotWin || PlayerWin)
        {
            Invoke(nameof(ResetDelay), 0.5f);
            ResetObjects();
        }
    }

    private void ResetObjects()
    {
        Time.timeScale = 0;
        matchTime = 0.0f;
        overtime = false;
        foreach (IResettable res in Global.resettables)
        {
            res.ResetObject();
        }
        Global.ruleManager.UpdatePlaystyle();
        Global.gameInfoUI.UpdateGameInfo();
        Time.timeScale = 1;
        TogglePause(true);
    }

    private void ResetDelay()
    {
        PlayerHealth.ResetObject();
        BotHealth.ResetObject();
    }

    private void Awake()
    {
        Global.gamemanager = this;
        TogglePause(true);
    }

    public void TogglePause(bool isPaused)
    {
        pauseCanvas.enabled = isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }
}
