using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private int InputHori = 0;
    private int InputVert = 0;

    [HideInInspector]
    public Vector2 InputVec = Vector2.zero;

    private void Update()
    {
        GetInputs();
        InputVec = new Vector2(InputHori, InputVert);
    }

    private void GetInputs()
    {
        InputHori = 0;
        InputVert = 0;

        // Handle Horizontal
        if (Input.GetKey(KeyCode.A))
        {
            InputHori -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            InputHori += 1;
        }

        // Handle Vertical
        if (Input.GetKey(KeyCode.W))
        {
            InputVert += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            InputVert -= 1;
        }
    }

}
