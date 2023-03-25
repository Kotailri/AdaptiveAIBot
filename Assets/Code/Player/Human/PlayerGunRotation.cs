using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunRotation : MonoBehaviour
{
    private Quaternion AngleFacing;

    public Quaternion GetAngleFacing()
    {
        return AngleFacing;
    }

    private void Update()
    {
        Vector3 VectorToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        float angle = (Mathf.Atan2(VectorToMouse.y, VectorToMouse.x) * Mathf.Rad2Deg) + 90;
        AngleFacing = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, AngleFacing, Time.deltaTime * 60);
    }
}
