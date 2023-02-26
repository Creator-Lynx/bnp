using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyToTargetMover : MonoBehaviour
{
    [SerializeField]
    Transform target;
    Rigidbody rig;
    [SerializeField]
    float speed;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Vector3 translation = target.position - transform.position;
        rig.velocity = translation * speed;
    }
}
