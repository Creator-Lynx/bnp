using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class FlowCurveHandler : MonoBehaviour
{
    float t = 0f;

    [SerializeField]
    Transform target, arrowsParent;
    [SerializeField]
    float lerpSpeed = 0.5f, findStep = 0.05f, offset = 0.5f;
    [SerializeField]
    float maxFlowDistance = 2f;
    [SerializeField]
    GameCurve curve;
    Quaternion currentRotation;
    public static Vector3 WaterVector;

    [SerializeField] GameObject flowArrowPrefab;
    void Start()
    {
        float locT = 0;
        int count = curve.SegmentsNumber * 3;
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = curve.GetPointByT(locT);
            float strong = pos.y;
            pos.y = -0.5f;
            Instantiate(flowArrowPrefab, pos,
            Quaternion.LookRotation(curve.GetDirectionByT(locT)), arrowsParent)
            .transform.localScale = new Vector3(1f, 1f, strong);
            locT += 1f / count;
        }
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
        float dist = (target.position - targetPos).magnitude;
        dist = (-dist + maxFlowDistance) / maxFlowDistance;
        if (dist < .1f) dist = .1f;
        WaterVector = dir.normalized * targetPos.y * dist;
    }

    void OnDrawGizmos()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 targetPos = curve.GetPointByT(t);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(targetPos, targetPos + dir * targetPos.y);
    }

}
