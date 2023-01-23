using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IResettable
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
        InitResettable();
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

    public void ResetObject()
    {
        transform.position = GameConfig.c_PlayerSpawnPosition;
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }
}
