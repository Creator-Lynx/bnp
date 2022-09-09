using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezie
{
    public static Vector3 GetPointNaive(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 p01 = Vector3.Lerp(p0, p1, t);
        Vector3 p12 = Vector3.Lerp(p1, p2, t);
        Vector3 p23 = Vector3.Lerp(p2, p3, t);

        Vector3 p012 = Vector3.Lerp(p01, p12, t);
        Vector3 p123 = Vector3.Lerp(p12, p23, t);

        Vector3 p0123 = Vector3.Lerp(p012, p123, t);

        return p0123;
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //F(t)=(1-t)^3*P0 + 3(1-t)^2 * t * P1 + 3*(1-t)*t^2*P2 + t^3 * P3;
        Vector3 result =
        Mathf.Pow(1 - t, 3) * p0 +
        3 * (1 - t) * (1 - t) * t * p1 +
        3 * (1 - t) * t * t * p2 +
        Mathf.Pow(t, 3) * p3;
        return result;
    }
    public static Vector3 GetPoint(Vector3[] p, float t)
    {
        //F(t)=(1-t)^3*P0 + 3(1-t)^2 * t * P1 + 3*(1-t)*t^2*P2 + t^3 * P3;
        Vector3 result =
        Mathf.Pow(1 - t, 3) * p[0] +
        3 * (1 - t) * (1 - t) * t * p[1] +
        3 * (1 - t) * t * t * p[2] +
        Mathf.Pow(t, 3) * p[3];
        return result;
    }

    public static Vector3 GetDirection(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        //F'(t) = 3(1-t)^2*(p1-p0)+6(1-t)t(p2-p1)+3t^2(p3-p2);
        Vector3 result =
        3 * (1 - t) * (1 - t) * (p1 - p0) +
        6 * (1 - t) * t * (p2 - p1) +
        3 * t * t * (p3 - p2);
        return result;
    }

    public static Vector3 GetDirection(Vector3[] p, float t)
    {
        //F'(t) = 3(1-t)^2*(p1-p0)+6(1-t)t(p2-p1)+3t^2(p3-p2);
        Vector3 result =
        3 * (1 - t) * (1 - t) * (p[1] - p[0]) +
        6 * (1 - t) * t * (p[2] - p[1]) +
        3 * t * t * (p[3] - p[2]);
        return result;
    }

    // public static float GetLerpBetweenTwoPointsByHandler(Vector3 point0, Vector3 point1, Vector3 handler)
    // {
    //
    //     Vector3 railVec = point1 - point0;
    //     Vector3 hadlerLocationAtRailStart = handler - point0;
    //
    //     //Vector3 pos = Vector3.Project(hadlerLocationAtRailStart, railVec);
    //     float dotPos = Vector3.Dot(hadlerLocationAtRailStart, railVec.normalized);
    //
    //     return dotPos / Vector3.Magnitude(railVec);
    // }
    // public static float GetClosestTByThreeVectors(Vector3[] p, Vector3 handler)
    // {
    //     float t = 0;
    //     int k = 0;
    //     float minSqrMagnitude = float.MaxValue;
    //     for (int i = 0; i < p.Length - 1; i++)
    //     {
    //         float temp = GetLerpBetweenTwoPointsByHandler(p[i], p[i + 1], handler);
    //         Vector3 point = (p[i + 1] - p[i]) * temp + p[i];
    //         float sqrMag = (handler - point).sqrMagnitude;
    //         if (sqrMag < minSqrMagnitude)
    //         {
    //             k = i;
    //             t = temp;
    //             minSqrMagnitude = sqrMag;
    //         }
    //
    //     }
    //     float dist = 0;
    //     for (int i = 0; i < k; i++)
    //     {
    //         dist += Vector3.Magnitude(p[i + 1] - p[i]);
    //     }
    //     dist += Vector3.Magnitude(p[k + 1] - p[k]) * t;
    //     float dist2 = 0;
    //     for (int i = 0; i < p.Length - 1; i++)
    //     {
    //         dist2 += Vector3.Magnitude(p[i + 1] - p[i]);
    //     }
    //     return dist / dist2;
    // }

    public static float GetClosestTOnCurveByPosition(Vector3[] p, Vector3 handler, int accuracy)
    {
        float minT = float.MaxValue;
        float dt = 1f / accuracy;
        float minSqrDst = float.MaxValue;
        for (int i = 0; i <= accuracy; i++)
        {
            Vector3 point = GetPoint(p[0], p[1], p[2], p[3], i * dt);

            float sqrDist = (handler - point).sqrMagnitude;
            if (sqrDist < minSqrDst)
            {
                minSqrDst = sqrDist;
                minT = i * dt;
            }
        }
        return minT;
    }
    public static float GetClosestTOnCurveByPosition(Vector3[] p, Vector3 handler, int accuracy, out float sqrDistance)
    {
        float minT = float.MaxValue;
        float dt = 1f / accuracy;
        float minSqrDst = float.MaxValue;
        for (int i = 0; i <= accuracy; i++)
        {
            Vector3 point = GetPoint(p[0], p[1], p[2], p[3], i * dt);

            float sqrDist = (handler - point).sqrMagnitude;
            if (sqrDist < minSqrDst)
            {
                minSqrDst = sqrDist;
                minT = i * dt;
            }
        }
        sqrDistance = minSqrDst;
        return minT;
    }
}
