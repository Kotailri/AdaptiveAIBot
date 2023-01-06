using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput inputs;
    private Rigidbody2D rb;

    void Start()
    {
        inputs = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = (inputs.InputVec.normalized) * GameConfig.c_PlayerMovespeed;
    }
}
