using UnityEngine;

public class TtestTargetDisappear : MonoBehaviour
{
    public float disapearTime;

    void Start()
    {
        Destroy(gameObject, disapearTime);
    }
}
