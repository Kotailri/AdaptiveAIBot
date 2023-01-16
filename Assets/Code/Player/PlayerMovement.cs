using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput inputs;
    private Rigidbody2D rb;

    private bool canMove = true;

    void Start()
    {
        inputs = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(7, 6);
    }

    void Update()
    {
        if (!canMove)
            return;

        rb.velocity = (inputs.InputVec.normalized) * GameConfig.c_PlayerMovespeed;
    }

    public void EnableMovement(bool enabled)
    {
        if (!enabled)
            rb.velocity = Vector2.zero;

        canMove = enabled;
    }
}
