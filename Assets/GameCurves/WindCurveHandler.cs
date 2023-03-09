using System;
using UnityEngine;

//[ExecuteAlways]
public class WindCurveHandler : MonoBehaviour
{
    public static float t = 0f;

    [SerializeField]
    Transform target, empty;
    [SerializeField]
    float lerpSpeed = 0.5f, findStep = 0.05f, offset = 0.5f, rotationLerpSpeed;
    [SerializeField]
    GameCurve curve;
    public static Quaternion currentRotation;
    public static Vector3 WindVector;

    void Start()
    {
        t = 0;
    }

    void Update()
    {
        SelfMoving();
        Vector3 targetPos = curve.GetPointByT(t);
        BezieSegment seg = curve.GetSegmentByT(t);
        Transform a = seg.points[0], b = seg.points[3];
        Quaternion targetRotation = Quaternion.Slerp(a.rotation, b.rotation, curve.GetLocalTbyT(t));
        float clampedLerpRate = Mathf.Clamp(Time.deltaTime * rotationLerpSpeed, 0.05f, 0.99f);
        currentRotation = Quaternion.Lerp(currentRotation, targetRotation, clampedLerpRate);
        empty.rotation = currentRotation;
        WindVector = empty.forward * targetPos.y;
    }
    void SelfMoving()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 tptr = (target.position - curve.GetPointByT(t)).normalized;
        float dotDist = Vector3.Dot(tptr, dir);
        if (dotDist > offset)
        {
            t = Mathf.Lerp(t, t + (findStep / curve.SegmentsNumber), Time.deltaTime * lerpSpeed);
        }

        //transform.position = curve.GetPointByT(t) + Vector3.back * 6f + Vector3.up * 4f;
    }


}
