using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteAlways]
public class BezieTest : MonoBehaviour
{
    [SerializeField]
    Transform[] points = new Transform[4];
    Vector3[] p = new Vector3[4];
    [SerializeField]
    [Range(0, 1)]
    float t;

    [SerializeField]
    Transform target;
    [SerializeField]
    float secondsToDestination = 0.5f, findStep = 0.05f;
    [SerializeField]
    int accuracy = 20;
    [SerializeField]
    GameCurve curve;
    void Start()
    {

    }

    void Update()
    {
        CurveMoving();
        //SelfBezie();
        //SelfMoving();
        transform.position = curve.GetPointByT(t) + Vector3.back * 7f + Vector3.up * 5f;
        BezieSegment seg = curve.GetSegmentByT(t);
        Transform a = seg.points[0], b = seg.points[3];
        transform.rotation = Quaternion.Lerp(a.rotation, b.rotation, curve.GetLocalTbyT(t));
    }
    void CurveMoving()
    {
        t = curve.GetClosestTByHandler(target.position);
        //transform.position = curve.GetPointByT(t);
        //transform.rotation = Quaternion.LookRotation(curve.GetDirectionByT(t));
    }
    void SelfMoving()
    {
        Vector3 dir = curve.GetDirectionByT(t);
        Vector3 tptr = (target.position - transform.position).normalized;
        float dotDist = Vector3.Dot(tptr, dir);
        if (dotDist > 0)
        {
            t = Mathf.Lerp(t, t + findStep, (1f / secondsToDestination) * Time.deltaTime);
        }

        //transform.position = curve.GetPointByT(t) + Vector3.back * 6f + Vector3.up * 4f;
    }

    void SelfBezie()
    {
        for (int i = 0; i < 4; i++)
        {
            p[i] = points[i].position;
        }
        //t = Bezie.GetLerpBetweenTwoPointsByHandler(p[0], p[3], target.position);
        //t = Bezie.GetLerpBetweenFourPointsByHandler(p, target.position);
        Vector3 tptr = (target.position - transform.position);
        Vector3 dir = Bezie.GetDirection(p[0], p[1], p[2], p[3], t).normalized;
        float dotDist = Vector3.Dot(tptr, dir);
        //if (dotDist > 0)
        //{
        //    t = Mathf.Lerp(t, t + findStep, (1f / seconsToDestination) * Time.deltaTime);
        //}
        t = Bezie.GetClosestTOnCurveByPosition(p, target.position, accuracy);
        //Debug.Log(t);
        transform.position = Bezie.GetPoint(p[0], p[1], p[2], p[3], t);
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + curve.GetDirectionByT(t));
        //int segmentsNumber = 20;
        //Vector3 preveousePoint = points[0].position;
        //for (int i = 0; i < segmentsNumber + 1; i++)
        //{
        //    float parameter = (float)i / segmentsNumber;
        //    Vector3 point = Bezie.GetPoint(p[0], p[1], p[2], p[3], parameter);
        //    Gizmos.DrawLine(preveousePoint, point);
        //    preveousePoint = point;
        //    //Gizmos.DrawCube(point, 0.5f * Vector3.one);
        //}
        //Vector3 dir = Bezie.GetDirection(p[0], p[1], p[2], p[3], t);
        //Vector3 pnt = Bezie.GetPoint(p[0], p[1], p[2], p[3], t);
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(pnt, pnt + dir);
    }
}
