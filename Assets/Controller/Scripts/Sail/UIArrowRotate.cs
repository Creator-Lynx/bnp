using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrowRotate : MonoBehaviour
{
    [SerializeField]
    SailMover player;

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float angle = Mathf.Asin(player.WindDirection.x) * Mathf.Rad2Deg;
            if (Mathf.Sign(player.WindDirection.z) < 0)
                angle = 180 - angle;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.05f);
        }
    }
}
