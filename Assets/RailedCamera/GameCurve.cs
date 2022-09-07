using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GameCurve : MonoBehaviour
{
    [SerializeField]
    Transform[] points;
    [SerializeField]
    int segmentsNumber;

    void Start()
    {

    }

    void Update()
    {
#if UNITY_EDITOR

        for (int i = 0; i < points.Length; i += 3)
        {

        }
#endif
    }

    public void SetControlsAuto()
    {

    }
    private void OnDrawGizmos()
    {

    }
}

public class BezieSegment
{
    public Transform[] points = new Transform[4];
    public Vector3[] GetPoints()
    {
        Vector3[] result = new Vector3[4];
        return result;
    }
}
