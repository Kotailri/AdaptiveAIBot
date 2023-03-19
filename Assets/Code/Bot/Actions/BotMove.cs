using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

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
    public LayerMask dodgeLayer;
    public LayerMask wallLayer;

    private Transform target;
    private NavMeshAgent agent;

    private MoveState moveState = MoveState.Follow;

    private Vector2 destination;
    private Vector2 currentSetMoveDestination;
    private bool canMove = true;
    private Vector2 currentVelocity;
    private Rigidbody2D RB;
    private BotDodge dodge;

    [HideInInspector]
    public bool destinationReached = false;

    private void Awake()
    {
        dodge = GetComponent<BotDodge>();
    }

    private void OnDrawGizmos()
    {
        if (target != null)
            Gizmos.DrawWireSphere(target.position, 7.0f);
    }

    public void ToggleCanMove(bool enabled)
    {
        canMove = enabled;
        if (moveState == MoveState.Move)
            agent.SetDestination(currentSetMoveDestination);
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetMove(float x, float y, float variance = 0.0f)
    {
        destinationReached = false;
        moveState = MoveState.Move;
        currentSetMoveDestination = new Vector2(x, y);

        if (variance >= 0.0f)
            currentSetMoveDestination = AddMoveVariance(currentSetMoveDestination, positionVariance: variance);

        if (Global.debugMode == true)
        {
            Instantiate(Global.gamemanager.testMarker, currentSetMoveDestination, Quaternion.identity);
        }
            
        agent.SetDestination(currentSetMoveDestination);
    }

    public Vector2 AddMoveVariance(Vector2 original, int maxIterations = 50, float positionVariance = 7.0f)
    {
        for (int i = 0; i < maxIterations; i++)
        {
            Vector2 position = Math.RandomInRadius(original, positionVariance);
            Collider2D[] wall = Physics2D.OverlapCircleAll(position, 0.6f, Global.gamemanager.wallLayer);
            if (wall.Length == 0)
            {
                return position;
            }
        }
        return original;
    }

    public void Attack()
    {
        moveState = MoveState.Follow;
    }

    public void Flee()
    {
        moveState = MoveState.Flee;
    }

    public void Stop()
    {
        moveState = MoveState.None;
        destinationReached = true;
    }

    public void MoveRandom()
    {
        Bounds worldBounds = GameConfig.c_WorldBounds;
        while (true)
        {
            Vector2 position = worldBounds.GenerateRandomPositionInBounds();
            Collider2D[] wall = Physics2D.OverlapCircleAll(position, 0.1f, Global.gamemanager.wallLayer);
            if (wall.Length == 0)
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
        RB = GetComponent<Rigidbody2D>();
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
        if (dodge.GetIncomingBullets())
        {
            agent.isStopped = true;
            dodge.isDodging = true;
        }
        else
        {
            dodge.isDodging = false;
            agent.isStopped = false;
            agent.enabled = true;

            if (!canMove || Debug_NoMove)
            {
                agent.SetDestination(transform.position);
                RB.velocity = Vector2.zero;
                return;
            }

            agent.speed = GameConfig.c_BotMovespeed + (Global.botSpeedBoost/2);

            switch (moveState)
            {
                case MoveState.Move:
                    if (Math.IsInRadius(currentSetMoveDestination, 1.0f, transform.position))
                    {
                        destinationReached = true;
                    }
                    break;

                case MoveState.Follow:
                    float offsetDistance = 2.2f;
                    Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
                    Vector3 targetPosition = target.transform.position - (directionToTarget * offsetDistance);
                    agent.SetDestination(targetPosition);
                    break;

                case MoveState.Flee:
                    agent.SetDestination(-(target.transform.position - (((target.transform.position - transform.position).normalized) * 2.2f)));
                    break;

                case MoveState.None:
                    agent.SetDestination(transform.position);
                    break;
            }
            RB.velocity = agent.velocity/5f;
            currentVelocity = agent.velocity;
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
