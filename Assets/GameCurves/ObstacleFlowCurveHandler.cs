using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFlowCurveHandler : MonoBehaviour
{
    float t = 0f;

    [SerializeField]
    Transform target;
    [SerializeField]
    float lerpSpeed = 0.5f, findStep = 0.05f, offset = 0.5f;
    [SerializeField]
    float maxFlowDistance = 2f, minForceAtDistanceThreshold = .5f;
    [SerializeField]
    GameCurve curve;

    public Vector3 WaterVector;
    public float ToFlowDistance;
    public Vector3 ToFlowForce;
    [SerializeField]
    AnimationCurve toFlowForceCurve;

    private void Start()
    {
        curve = FlowCurveHandler.FlowCurve;
        target = transform;
    }

    void Update()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 targetPos = curve.GetPointByT(t);
        Vector3 tptr = (target.position - curve.GetPointByT(t)).normalized;
        float dotDist = Vector3.Dot(tptr, dir);
        if (dotDist > offset)
        {
            t = Mathf.Lerp(t, t + (findStep / curve.SegmentsNumber), Time.deltaTime * lerpSpeed);
        }

        Vector3 vectorToFlow = (targetPos - new Vector3(target.position.x, targetPos.y, target.position.z));
        ToFlowDistance = vectorToFlow.magnitude;
        ToFlowForce = vectorToFlow / ToFlowDistance;
        ToFlowForce -= Vector3.Project(ToFlowForce, dir);
        //ToFlowForce.Normalize();
        ToFlowDistance = (-ToFlowDistance + maxFlowDistance) / maxFlowDistance;
        float toFlowK = 0;
        if (ToFlowDistance > 0) toFlowK = ToFlowDistance;
        toFlowK = toFlowForceCurve.Evaluate(toFlowK);
        ToFlowForce *= toFlowK;
        if (ToFlowDistance < minForceAtDistanceThreshold) ToFlowDistance = minForceAtDistanceThreshold;

        WaterVector = dir.normalized * targetPos.y * ToFlowDistance;
    }

}
