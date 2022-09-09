using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class CameraCurveMover : MonoBehaviour
{
    float t = 0f;

    [SerializeField]
    Transform target, followObject, rotatedObject;
    [SerializeField]
    float secondsToDestination = 0.5f, findStep = 0.05f, offset = 0.5f;
    [SerializeField]
    GameCurve curve;
    void Start()
    {

    }

    void Update()
    {
        SelfMoving();
        Vector3 targetPos = curve.GetPointByT(t);
        followObject.position = Vector3.Lerp(followObject.position, targetPos, 1 / secondsToDestination * Time.deltaTime);
        BezieSegment seg = curve.GetSegmentByT(t);
        Transform a = seg.points[0], b = seg.points[3];
        Quaternion targetRotation = Quaternion.Slerp(a.rotation, b.rotation, curve.GetLocalTbyT(t));
        rotatedObject.rotation = Quaternion.Lerp(rotatedObject.rotation, targetRotation, 1 / (secondsToDestination * Time.deltaTime * 4f));
        rotatedObject.localEulerAngles = new Vector3(rotatedObject.localEulerAngles.x, rotatedObject.localEulerAngles.y, 0f);
    }
    void SelfMoving()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 tptr = (target.position - curve.GetPointByT(t)).normalized;
        float dotDist = Vector3.Dot(tptr, dir);
        if (dotDist > offset)
        {
            t = Mathf.Lerp(t, t + (findStep / curve.SegmentsNumber), 1f / (secondsToDestination * Time.deltaTime));
        }

        //transform.position = curve.GetPointByT(t) + Vector3.back * 6f + Vector3.up * 4f;
    }


}
