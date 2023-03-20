using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    public CanvasGroup GameInfoUICanvas;

    [Space(10.0f)]
    public TextMeshProUGUI difficultyLevel;
    public Slider difficultyLevelSlider;

    [Space(10.0f)]
    public TextMeshProUGUI aggressionLevel;
    public Slider aggressionLevelSlider;

    [Space(10.0f)]
    public TextMeshProUGUI counterLevel;
    public Slider counterLevelSlider;

    [Space(10.0f)]
    public TextMeshProUGUI itemCollectLevel;
    public Slider itemCollectLevelSlider;

    [Space(10.0f)]
    public TextMeshProUGUI itemUsageLevel;
    public Slider itemUsageLevelSlider;

    [Space(10.0f)]
    public TextMeshProUGUI positionalLevel;
    public Slider positionalLevelSlider;

    [Space(25.0f)]
    public Button resetButton;

    [Space(10.0f)]
    public Toggle lockToggle;
    public Image lockToggleBg;

    private StateManager stateManager;

    private void Awake()
    {
        Global.gameInfoUI = this;
        UpdateGameInfo();
    }

    public void EnableUIElements(bool enabled)
    {
        if (enabled)
        {
            difficultyLevelSlider.interactable = true;
            aggressionLevelSlider.interactable = true;
            counterLevelSlider.interactable = true;
            itemCollectLevelSlider.interactable = true;
            itemUsageLevelSlider.interactable = true;
            positionalLevelSlider.interactable = true;
            resetButton.interactable = true;
            lockToggle.interactable = true;
            GameInfoUICanvas.alpha = 1;
        }
        else
        {
            difficultyLevelSlider.interactable = false;
            aggressionLevelSlider.interactable = false;
            counterLevelSlider.interactable = false;
            itemCollectLevelSlider.interactable = false;
            itemUsageLevelSlider.interactable = false;
            positionalLevelSlider.interactable = false;
            resetButton.interactable = false;
            lockToggle.interactable = false;
            GameInfoUICanvas.alpha = 0.5f;
        }
    }

    private void Start()
    {
        stateManager = Global.playertracker.Bot.GetComponent<StateManager>();

        difficultyLevelSlider.onValueChanged.AddListener((float val) =>
        {
            Global.difficultyLevel = val;
            difficultyLevel.text = string.Format("{0:F1}", val);
            stateManager.UpdateStatePriorities();
        });

        aggressionLevelSlider.onValueChanged.AddListener((float val) =>
        {
            Global.aggressionLevel = (int)val;
            aggressionLevel.text = val.ToString();
            stateManager.UpdateStatePriorities();
        });

        counterLevelSlider.onValueChanged.AddListener((float val) =>
        {
            Global.playerAttackCounterLevel = (int)val;
            counterLevel.text = val.ToString();
            stateManager.UpdateStatePriorities();
        });

        itemCollectLevelSlider.onValueChanged.AddListener((float val) =>
        {
            Global.playerItemCounterLevel = (int)val;
            itemCollectLevel.text = val.ToString();
            stateManager.UpdateStatePriorities();
        });

        itemUsageLevelSlider.onValueChanged.AddListener((float val) =>
        {
            Global.itemStrategyLevel = (int)val;
            itemUsageLevel.text = val.ToString();
            stateManager.UpdateStatePriorities();
        });

        positionalLevelSlider.onValueChanged.AddListener((float val) =>
        {
            Global.playerPositionCounterLevel = (int)val;
            positionalLevel.text = val.ToString();
            stateManager.UpdateStatePriorities();
        });

        resetButton.onClick.AddListener(() =>
        {
            Global.difficultyLevel = 0.0f;
            Global.aggressionLevel = 0;
            Global.playerAttackCounterLevel = 0;
            Global.playerItemCounterLevel = 0;
            Global.itemStrategyLevel = 0;
            Global.playerPositionCounterLevel = 0;
            Global.gamemanager.RestartGame();
            stateManager.UpdateStatePriorities();
            UpdateGameInfo();
            AudioManager.instance.PlaySound("click");
        });

        lockToggle.onValueChanged.AddListener((bool call) =>
        {
            Global.isLevelupLocked = call;
            lockToggleBg.color = call ? Color.red : Color.white;
            if (call)
                AudioManager.instance.PlaySound("lock");
            else
                AudioManager.instance.PlaySound("unlock");
        });
    }

    public void UpdateGameInfo()
    {
        difficultyLevel.text = string.Format("{0:F1}", Global.difficultyLevel);
        difficultyLevelSlider.value = Global.difficultyLevel;

        aggressionLevel.text = Global.aggressionLevel.ToString();
        aggressionLevelSlider.value = Global.aggressionLevel;

        counterLevel.text = Global.playerAttackCounterLevel.ToString();
        counterLevelSlider.value = Global.playerAttackCounterLevel;

        itemCollectLevel.text = Global.playerItemCounterLevel.ToString();
        itemCollectLevelSlider.value = Global.playerItemCounterLevel;

        itemUsageLevel.text = Global.itemStrategyLevel.ToString();
        itemUsageLevelSlider.value = Global.itemStrategyLevel;

        positionalLevel.text = Global.playerPositionCounterLevel.ToString();
        positionalLevelSlider.value = Global.playerPositionCounterLevel;

    }
}
