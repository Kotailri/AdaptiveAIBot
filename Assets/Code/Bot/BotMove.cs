using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum MoveState
{
    Follow,
    Flee,
    Move,
    None
}

public class BotMove : MonoBehaviour, IResettable
{
    public bool Debug_NoMove;

    private Transform target;
    private NavMeshAgent agent;

    private MoveState moveState = MoveState.Follow;

    private Vector2 destination;
    private bool canMove = true;

    private void OnDrawGizmos()
    {
        if (target != null)
            Gizmos.DrawWireSphere(target.position, 7.0f);
    }

    public void ToggleCanMove(bool enabled)
    {
        canMove = enabled;
    }

    public void SetMove(float x, float y)
    {
        moveState = MoveState.Move;
        destination = new Vector2(x, y);
    }

    public Vector2 AddMoveVariance(Vector2 original)
    {
        int maxIterations = 10;
        float positionVariance = 7.0f;

        Bounds worldBounds = GameConfig.c_WorldBounds;
        for (int i = 0; i < maxIterations; i++)
        {
            Vector2 position = Math.RandomInRadius(original, positionVariance);
            Collider2D wall = Physics2D.OverlapCircle(position, 0.3f, LayerMask.GetMask("Walls"));
            if (wall == null)
            {
                return position;
            }
        }
        return original;
    }

    public void Flee()
    {
        moveState = MoveState.Flee;
    }

    public void Attack()
    {
        moveState = MoveState.Follow;
    }

    public void Stop()
    {
        moveState = MoveState.None;
    }

    public void MoveRandom()
    {
        Bounds worldBounds = GameConfig.c_WorldBounds;
        while (true)
        {
            Vector2 position = worldBounds.GenerateRandomPositionInBounds();
            Collider2D wall = Physics2D.OverlapCircle(position, 0.1f, LayerMask.GetMask("Walls"));
            if (wall == null)
            {
                SetMove(position.x, position.y);
                return;
            }
        }
    }

    void OnDestroy()
    {
        OnDestroyAction();
    }

    void Start()
    {
        InitResettable();
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(7, 6);
        target = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        destination = target.transform.position;
    }

    void Update()
    {
        if (!canMove || Debug_NoMove)
        {
            agent.SetDestination(transform.position);
            return;
        }

        agent.speed = GameConfig.c_BotMovespeed + Global.botSpeedBoost;

        switch (moveState)
        {
            case MoveState.Move:
                agent.SetDestination(destination);
                break;

            case MoveState.Follow:
                agent.SetDestination(AddMoveVariance(target.transform.position));
                break;

            case MoveState.Flee:
                agent.SetDestination(AddMoveVariance((transform.position - target.position).normalized * 15.0f));
                break;

            case MoveState.None:
                agent.SetDestination(AddMoveVariance(transform.position));
                break;
        }
        
    }
    public void ResetObject()
    {
        transform.position = GameConfig.c_BotSpawnPosition;
    }

    public void InitResettable()
    {
        Global.resettables.Add(this);
    }

    public void OnDestroyAction()
    {
        Global.resettables.Remove(this);
    }
}
