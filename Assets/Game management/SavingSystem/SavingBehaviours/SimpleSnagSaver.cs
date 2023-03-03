using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSnagSaver : ISaveble
{
    Vector3 position;
    Quaternion rotation;
    public override void Load()
    {
        Debug.Log("Loading snag");
        transform.position = position;
        transform.rotation = rotation;
    }
    public override void Save()
    {
        Debug.Log("Saving snag");
        position = transform.position;
        rotation = transform.rotation;
    }
}
