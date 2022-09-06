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

    void Start()
    {

    }

    void Update()
    {


        for (int i = 0; i < 4; i++)
        {
            p[i] = points[i].position;
        }
        //t = Bezie.GetLerpBetweenTwoPointsByHandler(p[0], p[3], target.position);
        t = Bezie.GetLerpBetweenFourPointsByHandler(p, target.position);
        transform.position = Bezie.GetPoint(p[0], p[1], p[2], p[3], t);
        transform.rotation = Quaternion.LookRotation(Bezie.GetDirection(p[0], p[1], p[2], p[3], t));
    }

    private void OnDrawGizmos()
    {
        int segmentsNumber = 20;
        Vector3 preveousePoint = points[0].position;

        for (int i = 0; i < segmentsNumber + 1; i++)
        {
            float parameter = (float)i / segmentsNumber;
            Vector3 point = Bezie.GetPoint(p[0], p[1], p[2], p[3], parameter);
            Gizmos.DrawLine(preveousePoint, point);
            preveousePoint = point;
            //Gizmos.DrawCube(point, 0.5f * Vector3.one);
        }
    }
}
