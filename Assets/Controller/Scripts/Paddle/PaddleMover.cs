using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterSimulation))]
public class PaddleMover : MonoBehaviour
{
    public bool isUncontrolled = false;
    [SerializeField] float angleRotation = 0f;
    [SerializeField] float angleClamp = 30f;
    [SerializeField] float flowForce = 5f, rotateSpeed = 0.5f;

    [Space(30)]
    [SerializeField] Transform paddle;
    Vector3 moveByFlowDirection;
    WaterSimulation water;
    Rigidbody rig;
    void Start()
    {
        water = GetComponent<WaterSimulation>();
        rig = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (isUncontrolled) return;
        HandleDirection(Input.GetAxis("AltHorizontal"));
        if (Input.GetAxis("AltHorizontal") == 0)
            HandleDirection(JoysticksFacade.GetJoystick(JoystickName.right).GetHorizontalAxis());
    }

    /// <summary>
    /// Set rotation for boat movind along water moving, also rotate paddle
    /// </summary>
    /// <param name="Y - handle Y rotation parameter from -1 to 1, changing to angle of moving"></param>
    public void HandleDirection(float Y)
    {
        Y = Y / 2f + 0.5f;
        angleRotation = Mathf.SmoothStep(-angleClamp, angleClamp, Y);
        float radAngle = angleRotation * Mathf.Deg2Rad;
        /*
        rotating vector by angle, where x and y - coords of original vector
        rx = x cos α - y sin α
        ry = y cos α + x sin α
        */
        moveByFlowDirection = new Vector3(
        water.WaterFlowDirection.normalized.x * Mathf.Cos(radAngle) + water.WaterFlowDirection.normalized.z * Mathf.Sin(radAngle), 0f,
        water.WaterFlowDirection.normalized.z * Mathf.Cos(radAngle) - water.WaterFlowDirection.normalized.x * Mathf.Sin(radAngle));
        Debug.DrawRay(transform.position, moveByFlowDirection * 10, Color.blue);
    }

    void FixedUpdate()
    {
        ForceMoveByCurrentDirection();
        RotationByCurrentDirection();
    }

    void ForceMoveByCurrentDirection()
    {
        rig.AddForce(moveByFlowDirection * flowForce * water.WaterFlowDirection.magnitude, ForceMode.Force);
    }
    void RotationByCurrentDirection()
    {
        Quaternion targetRotation = Quaternion.Euler(0, Mathf.Asin(moveByFlowDirection.normalized.x) * Mathf.Rad2Deg, 0);
        Quaternion onlyFlowRotation = Quaternion.Euler(0, Mathf.Asin(water.WaterFlowDirection.normalized.x) * Mathf.Rad2Deg, 0);
        float t = Mathf.InverseLerp(0, .8f, FlowCurveHandler.ToFlowDistance);
        targetRotation = Quaternion.Lerp(onlyFlowRotation, targetRotation, t);
        //Quaternion invertTargetRotation = Quaternion.Euler(0, -Mathf.Asin(moveByFlowDirection.normalized.x) * Mathf.Rad2Deg, 0);
        Quaternion invertTargetRotation = Quaternion.Euler(0, -angleRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * 0.1f);
        if (paddle != null) paddle.transform.localRotation =
        Quaternion.Slerp(paddle.transform.localRotation, invertTargetRotation, rotateSpeed * 0.4f);
    }
}
