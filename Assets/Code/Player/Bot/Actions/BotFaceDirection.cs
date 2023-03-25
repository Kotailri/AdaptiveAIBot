using System.Collections.Generic;
using UnityEngine;

public class BotFaceDirection : MonoBehaviour, IActionHasUpdateAction, IActionExcludeState
{
    private float rotateSpeed = 15f;
    public void ExecuteAction()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > 0.01f)
        {
            Vector2 direction = GetComponent<Rigidbody2D>().velocity.normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    public List<ActionState> GetExcludedActionStates()
    {
        return new List<ActionState>() { ActionState.Attack };
    }
}

