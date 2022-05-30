using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterControllBehavior : MonoBehaviour, IControlable
{
    CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Move(Vector3 dir)
    {
        controller.Move(dir);
    }
    public void RotateY(float degree)
    {
        controller.transform.Rotate(Vector3.up * degree);
    }
}
