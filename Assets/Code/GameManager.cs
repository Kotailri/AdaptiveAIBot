using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool overtime = false;

    [Space(5.0f)]
    public Canvas pauseCanvas;
    public TextMeshProUGUI winnerTMP;

    [Space(5.0f)]
    public LayerMask wallLayer;
    public LayerMask detectorLayer;
    public GameObject testMarker;
    public bool DebugModeToggle;

    private GameInfoUI gameInfo;

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

        if (Input.GetKeyDown(KeyCode.Backspace) && SceneManager.GetActiveScene().name != "Menu")
        {
            SceneManager.LoadScene("Menu");
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
            yield return new WaitForSeconds(1.5f);
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
            winnerTMP.text = "Bot Wins...";
        }

        else if (PlayerWin)
        {
            AudioManager.instance.PlaySound("ding");
            PlayerScore++;
            playerScoreText.text = PlayerScore.ToString();
            Global.ruleManager.UpdateDifficulty(PlayerType.Player);
            winnerTMP.text = "Player Wins!";
        }

        if (BotWin || PlayerWin)
        {
            Invoke(nameof(ResetDelay), 0.1f);
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

        Global.playertracker.Bot.transform.position = GameConfig.c_BotSpawnPosition;
        Global.playertracker.Player.transform.position = GameConfig.c_PlayerSpawnPosition;
        
    }

    private void Awake()
    {
        Global.gamemanager = this;
        gameInfo = GetComponent<GameInfoUI>();
    }

    private void Start()
    {
        TogglePause(true);
    }

    public void TogglePause(bool isPaused)
    {
        // Reset keys
        Input.GetKeyUp(KeyCode.Space);
        Input.GetKeyUp(KeyCode.W);
        Input.GetKeyUp(KeyCode.A);
        Input.GetKeyUp(KeyCode.S);
        Input.GetKeyUp(KeyCode.D);
        Input.GetKeyUp(KeyCode.E);
        Input.GetKeyUp(KeyCode.Q);
        Input.GetMouseButtonUp(0);
        Input.GetMouseButtonUp(1);

        if (isPaused)
        {
            gameInfo.EnableUIElements(true);
            Time.timeScale = 0;
        }
        else
        {
            matchTime = 0.0f;
            gameInfo.EnableUIElements(false);
            Global.ruleManager.UpdatePlaystyle();
            Time.timeScale = 1;
        }

        pauseCanvas.enabled = isPaused;
        
    }
}
