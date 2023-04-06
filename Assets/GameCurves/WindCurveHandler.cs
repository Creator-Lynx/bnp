using System;
using UnityEngine;

//[ExecuteAlways]
public class WindCurveHandler : SelfSaver
{
    float t = 0f;

    [SerializeField]
    Transform target, empty;
    [SerializeField]
    float lerpSpeed = 0.5f, findStep = 0.05f, offset = 0.5f, rotationLerpSpeed;
    [SerializeField]
    GameCurve curve;
    public static Quaternion currentRotation;
    public static Vector3 WindVector;

    public override void Awake()
    {
        t = 0;
        base.Awake();
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
        tStat.AddKey(Time.time, t);
    }
    [SerializeField]
    AnimationCurve tStat;

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

    //SAVING ======================================
    float _windT;
    Quaternion _windRotation;
    protected override void Load()
    {
        t = _windT;
        Debug.Log("temporal saving wind " + _windT);
        WindCurveHandler.currentRotation = _windRotation;
    }

    protected override void Save()
    {
        _windT = t;
        _windRotation = WindCurveHandler.currentRotation;
    }
}
