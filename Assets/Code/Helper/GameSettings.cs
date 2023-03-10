using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public bool Debug;

    void Update()
    {
        Global.debugMode = Debug;
    }
}
