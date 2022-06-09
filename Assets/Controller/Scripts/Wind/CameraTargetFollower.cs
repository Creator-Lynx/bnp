using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetFollower : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Folow fo X and Z moving object with offset defined by start position of object and target.")]
    Transform target;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x + offset.x, transform.position.y, target.position.z + offset.z);
    }
}
