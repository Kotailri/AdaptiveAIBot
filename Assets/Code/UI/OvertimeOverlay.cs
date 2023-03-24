using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvertimeOverlay : MonoBehaviour
{
    CanvasGroup cg;
    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Global.gamemanager)
        {
            cg.alpha = Global.gamemanager.overtime ? 1 : 0;
        }
    }
}
