using System;
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
    float maxFlowDistance = 2f, minForceAtDistanceThreshold = .5f;
    [SerializeField]
    GameCurve curve;
    public static GameCurve FlowCurve
    {
        get
        {
            return instance.curve;
        }
        private set { }
    }
    static FlowCurveHandler instance;
    Quaternion currentRotation;
    public static Vector3 WaterVector;
    public static float ToFlowDistance;
    public static Vector3 ToFlowForce;
    [SerializeField]
    AnimationCurve toFlowForceCurve;

    [SerializeField] GameObject flowArrowPrefab;
    void Start()
    {

        if (instance != null) Debug.LogException(new Exception("More than one FlowCurveHandler on scene"));
        instance = this;
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

    void OnDrawGizmos()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 targetPos = curve.GetPointByT(t);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(targetPos, targetPos + dir.normalized * targetPos.y * ToFlowDistance);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(targetPos, new Vector3(target.position.x, targetPos.y, target.position.z));
    }

}
