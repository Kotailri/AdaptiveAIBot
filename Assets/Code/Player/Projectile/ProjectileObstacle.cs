using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Invisible obstacle that represents a projectile's trajectory.
/// </summary>
public class ProjectileObstacle : MonoBehaviour
{
    void Start()
    {
        if (Global.difficultyLevel < Random.Range(0.5f, 8.0f))
        {
            if (TryGetComponent(out NavMeshObstacle nav))
                nav.enabled = false;
        }
    }
}
