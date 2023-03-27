using UnityEngine;

public class TestTargetDisappear : MonoBehaviour
{
    public float disapearTime;

    void Start()
    {
        Destroy(gameObject, disapearTime);
    }
}
