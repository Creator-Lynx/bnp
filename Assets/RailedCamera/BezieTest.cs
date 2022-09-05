using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BezieTest : MonoBehaviour
{
    [SerializeField]
    Transform[] points = new Transform[4];
    Vector3[] p = new Vector3[4];
    [SerializeField]
    [Range(0, 1)]
    float t;

    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            p[i] = points[i].position;
        }
        transform.position = BezieCreator.GetPoint(p[0], p[1], p[2], p[3], t);
    }
}
