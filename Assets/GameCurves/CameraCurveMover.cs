using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class BezieTest : MonoBehaviour
{
    float t;

    [SerializeField]
    Transform target;
    [SerializeField]
    float secondsToDestination = 0.5f, findStep = 0.05f;
    [SerializeField]
    GameCurve curve;
    void Start()
    {

    }

    void Update()
    {
        SelfMoving();
        Vector3 targetPos = curve.GetPointByT(t);
        transform.position = Vector3.Lerp(transform.position, targetPos, 1 / secondsToDestination * Time.deltaTime);
        BezieSegment seg = curve.GetSegmentByT(t);
        Transform a = seg.points[0], b = seg.points[3];
        Quaternion targetRotation = Quaternion.Slerp(a.rotation, b.rotation, curve.GetLocalTbyT(t));
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1 / secondsToDestination * Time.deltaTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);
    }
    void SelfMoving()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 tptr = (target.position - curve.GetPointByT(t)).normalized;
        float dotDist = Vector3.Dot(tptr, dir);
        if (dotDist > 0)
        {
            t = Mathf.Lerp(t, t + findStep, (1f / secondsToDestination) * Time.deltaTime);
        }

        //transform.position = curve.GetPointByT(t) + Vector3.back * 6f + Vector3.up * 4f;
    }


}
