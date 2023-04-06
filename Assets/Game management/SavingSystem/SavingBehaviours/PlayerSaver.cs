using UnityEngine;
public class PlayerSaver : SelfSaver
{
    Vector3 position;
    Quaternion rotation;
    Vector3 velocity, rig_position, rig_angularVelocity;
    Rigidbody rig;
    //handeld direction of sail
    Vector3 _handeledDir;

    //set wind force randomizer
    //SailMover.RandomWindForceModifierState _randomState;
    //set flow force curve
    float _flowT;
    public override void Awake()
    {
        rig = GetComponent<Rigidbody>();
        base.Awake();
    }
    protected override void Save()
    {
        position = transform.position;
        rotation = transform.rotation;
        rig_position = rig.position;
        rig_angularVelocity = rig.angularVelocity;
        velocity = rig.velocity;
        _handeledDir = GetComponent<SailMover>().handledDirection;


        //water 
        _flowT = FlowCurveHandler.t;
    }
    protected override void Load()
    {
        transform.position = position;
        transform.rotation = rotation;
        rig.angularVelocity = rig_angularVelocity;
        rig.position = rig_position;
        rig.velocity = velocity;
        GetComponent<SailMover>().handledDirection = _handeledDir;

        //flow
        FlowCurveHandler.t = _flowT;
    }
}
