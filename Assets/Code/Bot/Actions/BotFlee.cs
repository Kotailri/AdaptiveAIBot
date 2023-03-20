using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BotFlee : MonoBehaviour, IActionHasInitialAction, IActionRequiredState, IActionHasStateCompletion, IActionHasUpdateAction
{
    public float maxDistance = 10f;
    public LayerMask obstacleLayer;
    public float searchRadius = 10f;

    private bool isActionCompleted = false;
    private bool findingSafeSpot = false;

    private Timer fleeTimer;

    public bool CheckAction()
    {
        return true;
    }

    public void ExecuteAction()
    {
        if (!findingSafeSpot)
        {
            GetComponent<BotMove>().Flee();
        }
            
        else if (GetComponent<BotMove>().destinationReached)
        {
            isActionCompleted = true;
        }
        
    }

    private void Start()
    {
        fleeTimer = null;
    }

    public void ExecuteInitialAction()
    {
        isActionCompleted = false;
        findingSafeSpot = true;

        Vector2 safePosition = GetClosestSafePosition();
        GetComponent<BotMove>().SetMove(safePosition.x, safePosition.y);

        if (Random.Range(0,2) == 0 && safePosition != Vector2.zero)
        {
            print(safePosition);
            findingSafeSpot = true;
            GetComponent<BotMove>().SetMove(safePosition.x, safePosition.y);
        }
        else
        {
            fleeTimer = new Timer(2.0f);
            findingSafeSpot = false;
        }

    }

    private void Update()
    {
        if (!findingSafeSpot && fleeTimer != null)
        {
            fleeTimer.IncrementTime(Time.deltaTime);
            if (fleeTimer.IsAvailable() && !isActionCompleted)
            {
                fleeTimer.PauseTimer(true);
                isActionCompleted = true;
            }
        }
    }

    public Vector2 GetClosestSafePosition()
    {
        GameObject player = Global.playertracker.Player;

        Vector2 directionToPlayer = (Vector2)player.transform.position - (Vector2)transform.position;
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        Vector2 safePosition = transform.position;
        int numPositionsToCheck = 8;
        float angleIncrement = 360f / numPositionsToCheck;
        float searchIncrement = searchRadius / numPositionsToCheck;

        for (int i = 0; i < numPositionsToCheck; i++)
        {
            float angle = i * angleIncrement;
            Vector2 rayDirection = Quaternion.AngleAxis(angle, Vector3.forward) * directionToPlayer;
            RaycastHit2D visionBlockers = Physics2D.Raycast(transform.position, rayDirection, distanceToPlayer, obstacleLayer);

            if (visionBlockers.collider != null)
            {
                safePosition = visionBlockers.point;

                Collider2D[] hazards = Physics2D.OverlapCircleAll(safePosition, 3f);
                foreach (Collider2D hazard in hazards)
                {
                    if (!hazard.CompareTag("Hazard"))
                    {
                        Collider2D wall = Physics2D.OverlapCircle(transform.position, 1.2f, Global.gamemanager.wallLayer);
                        if (wall == null)
                            return safePosition;
                    }
                }
            }
            safePosition += searchIncrement * rayDirection.normalized;
        }

        return Vector2.zero;
    }

    public List<ActionState> GetActionStates()
    {
        return new List<ActionState>() { ActionState.Flee };
    }

    public bool IsStateComplete()
    {
        return isActionCompleted;
    }
}
