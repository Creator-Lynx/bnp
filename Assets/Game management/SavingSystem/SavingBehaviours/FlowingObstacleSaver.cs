using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingObstacleSaver : ISaveble
{
    Vector3 position;
    Quaternion rotation;
    Vector3 velocity;
    float t;
    public override void Load()
    {
        Debug.Log("Loading snag");
        transform.position = position;
        transform.rotation = rotation;
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<ObstacleFlowCurveHandler>().t = t;
    }
    public override void Save()
    {
        Debug.Log("Saving snag");
        position = transform.position;
        rotation = transform.rotation;
        velocity = GetComponent<Rigidbody>().velocity;
        t = GetComponent<ObstacleFlowCurveHandler>().t;
    }
}
