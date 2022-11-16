using UnityEngine;

[RequireComponent(typeof(WaterSimulation))]
public class SailMover : MonoBehaviour
{
    public bool isUncontrolled = false;

    [SerializeField] float windForce = 2.5f, rotateSpeed = 0.5f;


    [Space(30)]
    [SerializeField] Transform sail;

    Rigidbody rig;

    Vector3 handledDirection = Vector3.forward;

    public Vector3 WindDirection = Vector3.forward;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
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


    }
    void FixedUpdate()
    {
        ForceMoveByCurrentDirection();
        RotationByCurrentDirection();

        WindDirection = WindCurveHandler.WindVector;
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
        rig.AddForce(resultForceDirection * windForce * (Time.fixedDeltaTime / 0.02f), ForceMode.Force);

    }

    void RotationByCurrentDirection()
    {
        float sailAngle = Mathf.Asin(handledDirection.x) * Mathf.Rad2Deg;
        if (Mathf.Sign(handledDirection.z) < 0)
            sailAngle = 180 - sailAngle;
        Quaternion sailRotation = Quaternion.Euler(0, sailAngle, 0);
        if (sail != null)
            sail.transform.localRotation = Quaternion.Slerp(sail.transform.localRotation, sailRotation, rotateSpeed * 0.1f * (Time.fixedDeltaTime / 0.02f));
    }
}
