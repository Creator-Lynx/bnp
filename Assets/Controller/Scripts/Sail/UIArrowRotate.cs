using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrowRotate : MonoBehaviour
{
    [SerializeField]
    SailMover player;

    Vector3 direction;
    void Update()
    {
        direction = player.WindDirection.normalized;
        if (player != null)
        {
            float angle = Mathf.Asin(direction.x) * Mathf.Rad2Deg;
            if (Mathf.Sign(direction.z) < 0)
                angle = 180 - angle;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
        }
    }
}
