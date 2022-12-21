using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(WaterSimulation))]
public class SailMover : MonoBehaviour
{
    public bool isUncontrolled = false;

    [SerializeField] float windForce = 2.5f, sailRotateSpeed = 0.5f;


    [Space(30)]
    [SerializeField]
    Transform sail;
    [Space(30)]
    [SerializeField]
    float threasholdRotation = 90f, forceRotatingAngle = 1f;
    [Space(30)]
    Rigidbody rig;

    Vector3 handledDirection = Vector3.forward;

    public Vector3 WindDirection = Vector3.forward;
    PlayerHitPointsController playerHP;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        playerHP = GetComponent<PlayerHitPointsController>();
        InitRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUncontrolled) return;
        HandleDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetAxis("Horizontal") == 0f && Input.GetAxis("Vertical") == 0f)
            HandleDirection(
                JoysticksFacade.GetJoystick(JoystickName.left).GetHorizontalAxis(),
                JoysticksFacade.GetJoystick(JoystickName.left).GetVerticalAxis());
        RandomTimerTick();
    }
    void FixedUpdate()
    {
        ForceMoveByCurrentDirection();
        RotationByCurrentDirection();
        interpolatedForceModifier = Mathf.Lerp(previousForceModifier, forceRandomModifier, tmr / randomForceTimer);
        WindDirection = WindCurveHandler.WindVector * interpolatedForceModifier;
        if (CheckThresholdRotation()) playerHP.SetDamage(1);
    }
    [SerializeField]
    float interpolatedForceModifier;
    //random system ==============================================================================================
    float forceRandomModifier = 0;
    float previousForceModifier = 0;
    float randomForceTimer = 1;
    Random.State randomState;
    [Space(30)]
    [SerializeField]
    float lowerRandomForceModifier = 0.25f;
    [SerializeField]
    float upperRandomForceModifier = 1.5f;
    [Space(5)]
    [SerializeField]
    float lowerRandomTimer = 1f;
    [SerializeField]
    float upperRandomTimer = 3f;
    void InitRandom()
    {
        Random.InitState(2341923);
        randomState = Random.state;
        RandomForceGenerate();
    }
    void RandomForceGenerate()
    {
        Random.state = randomState;
        previousForceModifier = forceRandomModifier;
        forceRandomModifier = Random.Range(lowerRandomForceModifier, upperRandomForceModifier);
        randomForceTimer = Random.Range(lowerRandomTimer, upperRandomTimer);
        randomState = Random.state;
        //Debug.Log(forceRandomModifier);
    }
    float tmr = 0f;
    void RandomTimerTick()
    {
        tmr += Time.deltaTime;
        if (tmr >= randomForceTimer)
        {
            tmr = 0;
            RandomForceGenerate();
        }
    }
    //end of random system =======================================================================================

    bool CheckThresholdRotation()
    {
        //if (Mathf.Abs(transform.localRotation.eulerAngles.x) +
        //    Mathf.Abs(transform.localRotation.eulerAngles.z) > threasholdRotation)
        //    return true;
        //return false;
        return Mathf.Abs(Vector3.Angle(Vector3.up, transform.up)) > threasholdRotation;

    }

    public void HandleDirection(float X, float Z)
    {
        if (X == 0 && Z == 0) return;
        Vector3 tmp = new Vector3(X, 0, Z).normalized;
        //Debug.DrawRay(transform.position + Vector3.up, tmp * 8, Color.white);
        handledDirection = Vector3.SlerpUnclamped(handledDirection, tmp, 0.2f);
        handledDirection.Normalize();

    }
    [SerializeField] Vector3 currentVel;

    void ForceMoveByCurrentDirection()
    {
        float dot = Vector3.Dot(WindDirection, handledDirection);
        //Debug.DrawRay(transform.position, WindDirection * 5, Color.magenta);
        float modifiedDot = dot >= 0 ? dot : 0f;
        Vector3 resultForceDirection = handledDirection * modifiedDot;
        //Debug.DrawRay(transform.position, resultForceDirection * 5f, Color.green);
        currentVel = Vector3.Project(rig.velocity, transform.forward);
        if (currentVel.z < 0f) return;
        rig.AddForce(resultForceDirection * windForce, ForceMode.Force);
        Vector3 torque = Vector3.Cross(Vector3.up, resultForceDirection);
        //rig.AddTorque(torque * windForce / 4f, ForceMode.Force);
        transform.RotateAround(sail.position, torque, forceRotatingAngle);
    }

    void RotationByCurrentDirection()
    {
        float sailAngle = Mathf.Asin(handledDirection.x) * Mathf.Rad2Deg;
        if (Mathf.Sign(handledDirection.z) < 0)
            sailAngle = 180 - sailAngle;
        Quaternion sailRotation = Quaternion.Euler(0, sailAngle, 0);
        if (sail != null)
            sail.transform.localRotation = Quaternion.Slerp(sail.transform.localRotation, sailRotation, sailRotateSpeed * 0.1f
            * (Time.fixedDeltaTime / 0.02f));
    }
}
