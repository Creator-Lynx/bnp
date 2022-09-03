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

        transform.position = GetPositionBetweenTwoPointsByHandler(points[0].position, points[1].position, player.position);


    }

    Vector3 GetPositionBetweenTwoPointsByHandler(Vector3 point0, Vector3 point1, Vector3 handler)
    {

        Vector3 railVec = point1 - point0;
        Vector3 hadlerLocationAtRailStart = handler - point0;

        Vector3 pos = Vector3.Project(hadlerLocationAtRailStart, railVec);
        //Vector3 pos = Vector3.Dot(hadlerLocationAtRailStart, railVec) * railVec;

        return pos;
    }
    void SetPositionAndRotationWithLerping()
    {
        Vector3 pos = GetPositionBetweenTwoPointsByHandler(points[0].position, points[1].position, player.position);
        transform.position = Vector3.Lerp(points[0].position, points[1].position,
        Vector3.Distance(pos, points[0].position) /
        Vector3.Distance(points[1].position, points[0].position)
        );

        transform.rotation = Quaternion.Lerp(points[0].rotation, points[1].rotation,
        Vector3.Distance(pos, points[0].position) /
        Vector3.Distance(points[1].position, points[0].position)
        );
        //transform.position = Vector3.Lerp(points[0].position, points[1].position,
        //(player.position.z - points[0].position.z) /
        //(points[1].position.z - points[0].position.z));
        //transform.rotation = Quaternion.Lerp(points[0].rotation, points[1].rotation,
        //(player.position.z - points[0].position.z) /
        //(points[1].position.z - points[0].position.z));
    }
}
