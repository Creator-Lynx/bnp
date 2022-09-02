using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSciptableFollow : MonoBehaviour
{
    [SerializeField]
    Transform[] points;

    [SerializeField]
    Transform player;


    void Update()
    {
        transform.position = Vector3.Lerp(points[0].position, points[1].position,
        (player.position.z - points[0].position.z) /
        (points[1].position.z - points[0].position.z));
        transform.rotation = Quaternion.Lerp(points[0].rotation, points[1].rotation,
        (player.position.z - points[0].position.z) /
        (points[1].position.z - points[0].position.z));
    }
}
