using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class PlayerDetector : MonoBehaviour
{
    private float timeSpent = 0.0f;
    private List<GameObject> optimalPoisonPositions = new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            optimalPoisonPositions.Add(child.gameObject);
        }
    }

    public List<GameObject> GetPoisonPositionList()
    {
        return optimalPoisonPositions;
    }

    public Transform GetPoisonPosition()
    {
        return optimalPoisonPositions[Random.Range(0, optimalPoisonPositions.Count)].gameObject.transform;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeSpent += Time.deltaTime;
        }
    }

    public float GetTimeSpent()
    {
        return timeSpent;
    }
}
