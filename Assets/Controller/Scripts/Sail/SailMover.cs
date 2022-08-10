using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaterSimulation))]
public class SailMover : MonoBehaviour
{

    [SerializeField] float moveSpeedFlow = 2.5f, windForce = 2.5f, rotateSpeed = 0.5f;

    [SerializeField] int randomizeWindTimer = 5;

    [Space(30)]
    [SerializeField] Transform sail;

    WaterSimulation water;
    Rigidbody rig;


    Vector3 handledDirection = Vector3.forward;

    public Vector3 WindDirection = Vector3.forward;
    void Start()
    {
        water = GetComponent<WaterSimulation>();
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        HandleDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        HandleDirection(
            JoysticksFacade.GetJoystick(JoystickName.left).GetHorizontalAxis(),
            JoysticksFacade.GetJoystick(JoystickName.left).GetVerticalAxis());


    }
    bool isChanged = false;
    void FixedUpdate()
    {
        ForceMoveByCurrentDirection();
        RotationByCurrentDirection();


        if (Time.time > 1)
            if ((int)Time.time % randomizeWindTimer == 0)
            {
                if (!isChanged)
                {
                    RandomizeWindDirection();
                    isChanged = true;
                }

            }
            else
            {
                isChanged = false;
            }
    }

    public void HandleDirection(float X, float Z)
    {
        if (X == 0 && Z == 0) return;
        Vector3 tmp = new Vector3(X, 0, Z).normalized;
        Debug.DrawRay(transform.position + Vector3.up, tmp * 8, Color.white);
        handledDirection = Vector3.SlerpUnclamped(handledDirection, tmp, 0.2f);
        handledDirection.Normalize();

    }

    void ForceMoveByCurrentDirection()
    {
        //rig.AddForce(water.WaterFlowDirection * moveSpeedFlow, ForceMode.Force);
        float dot = Vector3.Dot(WindDirection, handledDirection);
        Debug.DrawRay(transform.position, WindDirection * 5, Color.magenta);
        float modifiedDot = dot >= 0 ? dot : 0f;
        Vector3 resultForceDirection = handledDirection * modifiedDot;
        Debug.DrawRay(transform.position, resultForceDirection * 5f, Color.green);
        rig.AddForce(resultForceDirection * windForce, ForceMode.Force);
        rig.AddForce(water.WaterFlowDirection * moveSpeedFlow, ForceMode.Force);
    }

    void RotationByCurrentDirection()
    {
        float boatAngle = Mathf.Asin(water.WaterFlowDirection.x) * Mathf.Rad2Deg;
        if (Mathf.Sign(water.WaterFlowDirection.z) < 0)
            boatAngle = 180 - boatAngle;
        Quaternion boatRotation = Quaternion.Euler(0, boatAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, boatRotation, rotateSpeed * 0.1f);


        float sailAngle = Mathf.Asin(handledDirection.x) * Mathf.Rad2Deg;
        if (Mathf.Sign(handledDirection.z) < 0)
            sailAngle = 180 - sailAngle;
        Quaternion sailRotation = Quaternion.Euler(0, sailAngle, 0);
        if (sail != null)
            sail.transform.rotation = Quaternion.Slerp(sail.transform.rotation, sailRotation, rotateSpeed * 0.1f);
    }

    void RandomizeWindDirection()
    {
        Vector3 tmp = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        WindDirection = tmp.normalized;
    }

}
