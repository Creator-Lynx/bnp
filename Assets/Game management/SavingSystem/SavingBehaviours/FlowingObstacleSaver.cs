using UnityEngine;

public class FlowingObstacleSaver : SelfSaver
{
    Vector3 position;
    Quaternion rotation;
    Vector3 velocity;
    float t;
    protected override void Load()
    {
        transform.position = position;
        transform.rotation = rotation;
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<ObstacleFlowCurveHandler>().t = t;
    }
    protected override void Save()
    {
        position = transform.position;
        rotation = transform.rotation;
        velocity = GetComponent<Rigidbody>().velocity;
        t = GetComponent<ObstacleFlowCurveHandler>().t;
    }
}
