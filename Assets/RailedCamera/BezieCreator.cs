using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BezieCreator
{
    public static Vector3 GetPoint(Vector3 a0, Vector3 c0, Vector3 c1, Vector3 a1, float t)
    {
        Vector3 ac0 = Vector3.Lerp(a0, c0, t);
        Vector3 c01 = Vector3.Lerp(c0, c1, t);
        Vector3 ca1 = Vector3.Lerp(c1, a1, t);

        Vector3 a0c01 = Vector3.Lerp(ac0, c01, t);
        Vector3 c01a1 = Vector3.Lerp(c01, ca1, t);

        Vector3 c01a01 = Vector3.Lerp(a0c01, c01a1, t);

        return c01a01;
    }
}
