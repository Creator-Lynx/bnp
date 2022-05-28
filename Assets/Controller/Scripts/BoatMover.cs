using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMover : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotateSpeed = 5f;
    public bool isFlow = true;
    [SerializeField] Vector3 flowDirection = new Vector3(0, 0, 1);
    [SerializeField] float flowSpeed = 0f;
    public float horizontal = 0f;
    CharacterController controller;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        if (isFlow)
        {
            controller.Move(transform.forward * speed * Time.fixedDeltaTime +
            flowDirection * flowSpeed * Time.fixedDeltaTime);
        }
        if (horizontal != 0f)
        {
            controller.transform.Rotate(Vector3.up * horizontal * rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
