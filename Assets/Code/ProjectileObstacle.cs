using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileObstacle : MonoBehaviour
{
    void Start()
    {
        if (Global.difficultyLevel < Random.Range(0.5f, 8.0f))
        {
            GetComponent<NavMeshObstacle>().enabled = false;
        }
    }
}
