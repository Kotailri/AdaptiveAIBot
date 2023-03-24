using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [System.Serializable]
    public class SceneButton
    {
        public Button button;
        public string sceneName;

        [Space(10.0f)]
        [Header("UI")]
        public bool UILocked;

        [Header("Difficulty")]
        public float defaultDifficulty;

        [Space(5.0f)]
        public bool difficultyLocked;

        [Header("Playstyles")]
        public int defaultAggression;
        public int defaultCounter;
        public int defaultItemCollect;
        public int defaultItemUsage;
        public int defaultPositional;

        [Space(5.0f)]
        public bool playstyleLocked;
    }

    public List<SceneButton> buttons = new List<SceneButton> ();

    private void Awake()
    {
        foreach (SceneButton sb in buttons)
        {
            sb.button.onClick.AddListener(() =>
            {
                Global.difficultyLevel = sb.defaultDifficulty;
                Global.aggressionLevel = sb.defaultAggression;
                Global.playerAttackCounterLevel = sb.defaultCounter;
                Global.playerItemCounterLevel = sb.defaultItemUsage;
                Global.itemStrategyLevel = sb.defaultItemCollect;
                Global.playerPositionCounterLevel = sb.defaultPositional;

                Global.playstyleLocked = sb.playstyleLocked;

                Global.difficultyLocked = sb.difficultyLocked;
                Global.UILocked = sb.UILocked;

                AudioManager.instance.PlaySound("click");

                SceneManager.LoadScene(sb.sceneName);
            });
        }
    }
}
