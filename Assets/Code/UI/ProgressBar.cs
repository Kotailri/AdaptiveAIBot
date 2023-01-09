using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/ProgressBar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/ProgressBar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
    public int max;
    public int min;
    public int current;

    public Image mask;
    public Image fill;
    public Color color;

    [Space(5)]
    public List<Color> colChange = new List<Color>();
    public List<float> percentChange = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currOffset = current - min;
        float maxOffset = max - min;
        float fillAmount = (float)currOffset / (float)maxOffset;
        mask.fillAmount = fillAmount;
        for (int i = 0; i < percentChange.Count && i < colChange.Count; i++)
        {
            if (fillAmount <= percentChange[i])
            {
                fill.color = colChange[i];
                return;
            }
        }
        
        fill.color = color;
    }
}
