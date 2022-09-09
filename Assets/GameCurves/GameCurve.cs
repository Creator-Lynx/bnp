using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GameCurve : MonoBehaviour
{
    [SerializeField]
    Transform[] points;
    [SerializeField]
    int segmentsNumber = 2, accuracy = 20;
    public int SegmentsNumber { get { return segmentsNumber; } }
    [SerializeField]
    [Range(0, 1)]
    float autoControlLength = 0.5f;
    [SerializeField]
    BezieSegment[] segments;

    void Start()
    {
        AutoSetAllControls();
        segments = new BezieSegment[segmentsNumber];
        for (int i = 0; i < points.Length - 1; i += 3)
        {
            segments[i / 3] = new BezieSegment(points, i);
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        AutoSetAllControls();

#endif
    }
    public BezieSegment GetSegmentByT(float t)
    {
        int i = Mathf.FloorToInt(t * segmentsNumber);
        if (i == segmentsNumber) i--;
        return segments[i];
    }
    public float GetLocalTbyT(float t)
    {
        int i = Mathf.FloorToInt(t * segmentsNumber);
        if (i == segmentsNumber) i--;
        float dt = t * segmentsNumber - i;
        return dt;
    }
    public Vector3 GetPointByT(float t)
    {
        int i = Mathf.FloorToInt(t * segmentsNumber);
        if (i < 0) i = 0;
        if (i == segmentsNumber) i--;
        float dt = t * segmentsNumber - i;
        return Bezie.GetPoint(segments[i].GetPoints(), dt);
    }
    public Vector3 GetDirectionByT(float t)
    {
        int i = Mathf.FloorToInt(t * segmentsNumber);
        if (i == segmentsNumber) i--;
        float dt = t * segmentsNumber - i;
        return Bezie.GetDirection(segments[i].GetPoints(), dt);
    }
    public float GetClosestTByHandler(Vector3 handler)
    {
        float minDist = float.MaxValue;
        int minI = 0;
        float minT = 0;
        float tempDist = 0;
        for (int i = 0; i < segmentsNumber; i++)
        {
            float tempT = Bezie.GetClosestTOnCurveByPosition(segments[i].GetPoints(), handler, accuracy, out tempDist);
            if (tempDist < minDist)
            {
                minDist = tempDist;
                minT = tempT;
                minI = i;
            }
        }
        return (minI * (1f / segmentsNumber)) + minT * (1f / segmentsNumber);
    }
    public void AutoSetAllControls()
    {
        if (segmentsNumber > 1)
        {
            for (int i = 3; i < points.Length - 1; i += 3)
            {
                AutoSetAnchorControls(i);
            }
        }
        AutoSetStartAndEndAnchorsControls();
    }
    void AutoSetAnchorControls(int anchorIndex)
    {
        Vector3 ancorPos = points[anchorIndex].position;
        Vector3 dir = Vector3.zero;
        float leftDistance, rightDistance;
        Vector3 offset;

        offset = points[anchorIndex - 3].position - ancorPos;
        dir += offset.normalized;
        leftDistance = offset.magnitude;

        offset = points[anchorIndex + 3].position - ancorPos;
        dir -= offset.normalized;
        rightDistance = -offset.magnitude;

        dir.Normalize();

        points[anchorIndex - 1].position = ancorPos + dir * leftDistance * autoControlLength;

        points[anchorIndex + 1].position = ancorPos + dir * rightDistance * autoControlLength;

    }
    void AutoSetStartAndEndAnchorsControls()
    {
        if (segmentsNumber == 1)
        {
            points[1].position = points[0].position + (points[3].position - points[0].position) * .25f;
            points[2].position = points[3].position + (points[0].position - points[3].position) * .25f;
        }
        else
        {
            points[1].position = (points[0].position + points[2].position) * .5f;
            points[points.Length - 2].position = (points[points.Length - 1].position + points[points.Length - 3].position) * .5f;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        for (int j = 0; j < segmentsNumber; j++)
        {
            if (j % 2 != 0) Gizmos.color = Color.green;
            else Gizmos.color = Color.magenta;
            int segmentsNum = 20;
            Vector3 preveousePoint = segments[j].points[0].position;

            for (int i = 0; i < segmentsNum + 1; i++)
            {
                float parameter = (float)i / segmentsNum;
                Vector3 point = Bezie.GetPoint(segments[j].GetPoints(), parameter);
                Gizmos.DrawLine(preveousePoint, point);
                preveousePoint = point;
            }
        }
    }
#endif
}

[System.Serializable]
public class BezieSegment
{
    public Transform[] points = new Transform[4];
    public BezieSegment(Transform[] p, int i)
    {
        for (int j = i; j < i + 4; j++)
        {
            points[j - i] = p[j];
        }
    }

    public Vector3[] GetPoints()
    {
        Vector3[] result = new Vector3[4];
        for (int i = 0; i < 4; i++)
        {
            result[i] = points[i].position;
        }
        return result;
    }
}
