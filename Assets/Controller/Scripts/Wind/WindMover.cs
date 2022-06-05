using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindMover : MonoBehaviour
{

    [SerializeField] float force = 5f;
    //IControlable controller;
    [SerializeField] Vector3 forceDeltaDirection = Vector3.forward;
    [Space]
    [SerializeField] Vector3 centerOfMass = Vector3.zero;

    [SerializeField] float rotateSpeed = 0.5f;

    Rigidbody rig;
    void Start()
    {
        //controller = GetComponent<IControlable>();
        rig = GetComponent<Rigidbody>();
    }

    float targetRotation = 0f;
    void FixedUpdate()
    {
        //controller.Move(transform.forward * speed * Time.deltaTime);
        rig.centerOfMass = centerOfMass;
        // transform.Rotate(0f,  * rotateSpeed, 0f);
    }

    public void CreateWindByPos(Vector3 pos)
    {
        pos.y = transform.position.y;
        Vector3 dir = (transform.position - pos).normalized;
        rig.AddForce(dir * force, ForceMode.Impulse);
        transform.rotation = Quaternion.Euler(0f, Vector3.SignedAngle(Vector3.forward, dir, Vector3.up), 0f);
        //targetRotation = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
    }
}
