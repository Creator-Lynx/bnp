using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WindMover : MonoBehaviour
{

    [SerializeField]
    float speed = 5f;
    //IControlable controller;
    [SerializeField]
    Vector3 forceDeltaDirection = Vector3.forward;

    Rigidbody rig;
    void Start()
    {
        //controller = GetComponent<IControlable>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //controller.Move(transform.forward * speed * Time.deltaTime);
    }

    public void CreateWindByPos(Vector3 pos)
    {
        rig.AddForceAtPosition(forceDeltaDirection * speed, pos, ForceMode.Acceleration);
    }
}
