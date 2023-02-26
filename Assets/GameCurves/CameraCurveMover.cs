using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class CameraCurveMover : MonoBehaviour
{
    public float t = 0f;

    [SerializeField]
    Transform target, followObject, rotatedObject, springFollower;
    [SerializeField]
    float lerpSpeed = 0.5f, findStep = 0.05f, offset = 0.5f, rotationLerpSpeed, springFollowerLerpRate = 15;
    [SerializeField]
    GameCurve curve;

    void LateUpdate()
    {
        SelfMoving();
        Vector3 targetPos = curve.GetPointByT(t);
        followObject.position = Vector3.Lerp(followObject.position, targetPos, Time.deltaTime * lerpSpeed);
        BezieSegment seg = curve.GetSegmentByT(t);
        Transform a = seg.points[0], b = seg.points[3];
        Quaternion targetRotation = Quaternion.Lerp(a.rotation, b.rotation, curve.GetLocalTbyT(t));
        rotatedObject.rotation = Quaternion.Lerp(rotatedObject.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        rotatedObject.localEulerAngles = new Vector3(rotatedObject.localEulerAngles.x, rotatedObject.localEulerAngles.y, 0f);
        rotatedObject.transform.position = Vector3.Lerp(rotatedObject.position, springFollower.position, springFollowerLerpRate * Time.deltaTime);
    }
    void SelfMoving()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 tptr = target.position - curve.GetPointByT(t);
        float dotDist = Vector3.Dot(tptr, dir);
        if (dotDist > offset)
        {
            t = Mathf.Lerp(t, t + (findStep / curve.SegmentsNumber), Time.deltaTime * lerpSpeed);
        }

        //transform.position = curve.GetPointByT(t) + Vector3.back * 6f + Vector3.up * 4f;
    }


}
