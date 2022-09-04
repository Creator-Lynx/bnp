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

        for (int i = 1; i < points.Length; i++)
        {
            float t = GetLerpBetweenTwoPointsByHandler(points[i - 1].position, points[i].position, player.position);
            if (t <= 1 && t >= 0)
            {
                SetPositionAndRotationWithLerping(points[i - 1], points[i], t);
                break;
            }
        }



    }

    float GetLerpBetweenTwoPointsByHandler(Vector3 point0, Vector3 point1, Vector3 handler)
    {

        Vector3 railVec = point1 - point0;
        Vector3 hadlerLocationAtRailStart = handler - point0;

        //Vector3 pos = Vector3.Project(hadlerLocationAtRailStart, railVec);
        float dotPos = Vector3.Dot(hadlerLocationAtRailStart, railVec.normalized);

        return dotPos / Vector3.Magnitude(railVec);
    }
    void SetPositionAndRotationWithLerping(Transform point0, Transform point1, float dotPos)
    {
        transform.position = Vector3.Lerp(point0.position, point1.position, dotPos);
        transform.rotation = Quaternion.Lerp(point0.rotation, point1.rotation, dotPos);
    }
}
