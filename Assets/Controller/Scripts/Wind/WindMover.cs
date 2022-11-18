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
    float rotSign = 0f;
    void FixedUpdate()
    {
        rig.centerOfMass = centerOfMass;

        //if (transform.rotation.y > 180) transform.rotation = Quaternion.Euler(transform.rotation.x, -180, transform.rotation.z);
        //if (transform.rotation.y < -180) transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotation, 0), rotateSpeed * 0.1f * 50 * Time.deltaTime);
        //transform.Rotate(0f, rotSign * rotateSpeed, 0f);
    }
    Vector3 dir = Vector3.zero;
    public void CreateWindByPos(Vector3 pos)
    {
        pos.y = transform.position.y;
        dir = (transform.position - pos).normalized;
        rig.AddForce(dir * force, ForceMode.Impulse);
        targetRotation = Vector3.SignedAngle(Vector3.forward, dir, Vector3.up);
        rotSign = Mathf.Sign(Mathf.Sin(Vector3.SignedAngle(transform.forward, dir, Vector3.up) * Mathf.Deg2Rad));
        // transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
    }
}
