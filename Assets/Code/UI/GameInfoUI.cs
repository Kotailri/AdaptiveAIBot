using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    public TextMeshProUGUI difficultyLevel;

    public TextMeshProUGUI aggressionLevel;
    public TextMeshProUGUI counterLevel;
    public TextMeshProUGUI itemCollectLevel;
    public TextMeshProUGUI itemUsageLevel;
    public TextMeshProUGUI positionalLevel;

    [Space(10.0f)]
    public Button resetButton;

    private void Awake()
    {
        Global.gameInfoUI = this;
    }

    private void Start()
    {
        resetButton.onClick.AddListener(() =>
        {
            Global.difficultyLevel = 0.0f;
            Global.aggressionLevel = 0;
            Global.playerAttackCounterLevel = 0;
            Global.playerItemCounterLevel = 0;
            Global.itemStrategyLevel = 0;
            Global.playerPositionCounterLevel = 0;
            Global.gamemanager.RestartGame();
            UpdateGameInfo();
        });
    }

    public void UpdateGameInfo()
    {
        difficultyLevel.text = string.Format("{0:F1}", Global.difficultyLevel);

        aggressionLevel.text = Global.aggressionLevel.ToString();
        counterLevel.text = Global.playerAttackCounterLevel.ToString();
        itemCollectLevel.text = Global.playerItemCounterLevel.ToString();
        itemUsageLevel.text = Global.itemStrategyLevel.ToString();
        positionalLevel.text = Global.playerPositionCounterLevel.ToString();
    }
}
