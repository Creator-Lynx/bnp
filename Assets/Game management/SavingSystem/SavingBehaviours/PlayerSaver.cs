using UnityEngine;

public class PlayerSaver : SelfSaver
{
    Vector3 position;
    Quaternion rotation;
    Vector3 velocity;
    //handeld direction of sail
    Vector3 _handeledDir;
    //set wind direction curve
    //set wind force randomizer
    //set flow force curve

    protected override void Save()
    {
        position = transform.position;
        rotation = transform.rotation;
        velocity = GetComponent<Rigidbody>().velocity;
        _handeledDir = GetComponent<SailMover>().handledDirection;
    }
    protected override void Load()
    {
        transform.position = position;
        transform.rotation = rotation;
        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<SailMover>().handledDirection = _handeledDir;
    }
}
