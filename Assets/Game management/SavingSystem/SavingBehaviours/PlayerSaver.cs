using UnityEngine;
public class PlayerSaver : SelfSaver
{
    Vector3 position;
    Quaternion rotation;
    Vector3 velocity;
    //handeld direction of sail
    Vector3 _handeledDir;
    //set wind direction curve
    float _windT;
    Quaternion _windRotation;
    //set wind force randomizer
    SailMover.RandomWindForceModifierState _randomState;
    //set flow force curve
    float _flowT;
    protected override void Save()
    {
        position = transform.position;
        rotation = transform.rotation;
        velocity = GetComponent<Rigidbody>().velocity;
        _handeledDir = GetComponent<SailMover>().handledDirection;
        //wind
        _windT = WindCurveHandler.t;
        _windRotation = WindCurveHandler.currentRotation;
        _randomState = GetComponent<SailMover>().CurrentRandomState;
        //water 
        _flowT = FlowCurveHandler.t;
    }
    protected override void Load()
    {
        transform.position = position;
        transform.rotation = rotation;
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<SailMover>().handledDirection = _handeledDir;
        //wind
        WindCurveHandler.t = _windT;
        WindCurveHandler.currentRotation = _windRotation;
        GetComponent<SailMover>().CurrentRandomState = _randomState;
        //flow
        FlowCurveHandler.t = _flowT;
    }
}
