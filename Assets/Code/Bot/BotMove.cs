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

    [HideInInspector]
    public bool destinationReached = false;

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

    public void SetMove(float x, float y, float variance = 0.0f)
    {
        destinationReached = false;
        moveState = MoveState.Move;
        currentSetMoveDestination = new Vector2(x, y);

        if (variance >= 0.0f)
            currentSetMoveDestination = AddMoveVariance(currentSetMoveDestination, positionVariance: variance);

        agent.SetDestination(currentSetMoveDestination);
    }

    public Vector2 AddMoveVariance(Vector2 original, int maxIterations = 10, float positionVariance = 7.0f)
    {
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
        destinationReached = true;
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
        RB = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(7, 6);
        target = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        destination = target.transform.position;
    }

    private void DodgeObstacles(Collider2D[] obstacles)
    {
        // Calculate the dodge direction based on the average of all unobstructed obstacle normals
        Vector2 dodgeDirection = Vector2.zero;
        int validObstacles = 0;
        for (int i = 0; i < obstacles.Length; i++)
        {
            // Check if the obstacle is obstructed by a wall
            Vector2 obstacleDirection = obstacles[i].transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, obstacleDirection, obstacleDirection.magnitude, wallLayer);
            if (hit.collider == null)
            {
                dodgeDirection += Vector2.Perpendicular(obstacleDirection.normalized);
                validObstacles++;
            }
        }
        if (validObstacles > 0)
        {
            dodgeDirection /= validObstacles;

            // Calculate the steering force for the dodge
            Vector2 steeringForce = dodgeDirection.normalized * 25f;

            // Add the dodge steering force to the current velocity
            currentVelocity += steeringForce * Time.deltaTime;

            // Limit the current velocity to the agent's maximum speed
            currentVelocity = Vector2.ClampMagnitude(currentVelocity, agent.speed);

            // Set the agent's velocity to the current velocity
            agent.velocity = currentVelocity / 5;
        }
    }

    void Update()
    {
        if (!canMove || Debug_NoMove)
        {
            agent.SetDestination(transform.position);
            RB.velocity = Vector2.zero;
            return;
        }

        agent.speed = GameConfig.c_BotMovespeed + Global.botSpeedBoost;

        // Detect all obstacles within the avoid distance
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, 5f, dodgeLayer);
        if (obstacles.Length > 0)
        {
            agent.SetDestination(transform.position);
            DodgeObstacles(obstacles);
        }
        else
        {
            switch (moveState)
            {
                case MoveState.Move:
                    if (Math.IsInRadius(currentSetMoveDestination, 3.0f, transform.position))
                    {
                        destinationReached = true;
                    }
                    break;

                case MoveState.Follow:
                    agent.SetDestination(target.transform.position);
                    break;

                case MoveState.Flee:
                    agent.SetDestination((transform.position - target.position).normalized * 15.0f);
                    break;

                case MoveState.None:
                    agent.SetDestination(transform.position);
                    break;
            }
            RB.velocity = agent.velocity/5;
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
