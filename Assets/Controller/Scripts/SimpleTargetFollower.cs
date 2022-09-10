using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTargetFollower : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Folow fo X and Z moving object with offset defined by start position of object and target.")]
    Transform target;
    float offset;
    float yOffset;
    void Start()
    {
        Vector3 nullYpos = new Vector3(target.position.x, yOffset, target.position.z);
        offset = (transform.position - nullYpos).magnitude;
        yOffset = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(target.position.x + sqrOffset.x, transform.position.y, target.position.z + sqrOffset.z);
        Vector3 nullYpos = new Vector3(target.position.x, yOffset, target.position.z);
        Vector3 dir = transform.position - nullYpos;
        transform.position = nullYpos + dir.normalized * offset;
    }
}
