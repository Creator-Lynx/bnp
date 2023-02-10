using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObstacleWaterSimulation : MonoBehaviour
{
    [SerializeField] public float WaterDensity = 10f, ToFlowForceValue = 5f;

    [SerializeField] Vector3 forceDirection = Vector3.up;

    [SerializeField]
    float rig_drag = 2f, rig_angularDrag = 2f;
    public Vector3 WaterFlowDirection = Vector3.forward;

    Rigidbody rig;
    ObstacleFlowCurveHandler _curveHandler;
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        _curveHandler = GetComponent<ObstacleFlowCurveHandler>();
    }

    public float divePercent;
    int fixedCounter = 0;
    void FixedUpdate()
    {


        WaterFlowDirection = _curveHandler.WaterVector;


        divePercent = -transform.position.y;
        divePercent = Mathf.Clamp(divePercent, 0f, 1f);
        float WaterDensityMod = WaterDensity;// + Mathf.Sin(Time.time) * 5;

        Debug.Log(Time.time + "\n" + Mathf.Sin(Time.time * Mathf.PI * 2));
        rig.AddForce(forceDirection * divePercent * WaterDensityMod);
        rig.AddForce(_curveHandler.ToFlowForce * ToFlowForceValue);
        rig.drag = divePercent * rig_drag;
        rig.angularDrag = divePercent * rig_angularDrag;
        if (divePercent > 0.25f)
            ForceMoveByCurrentDirection();
        RotationByCurrentDirection();
        fixedCounter++;
    }


    [SerializeField] float flowForce = 5f, rotateSpeed = 0.5f;
    void ForceMoveByCurrentDirection()
    {
        rig.AddForce(flowForce * WaterFlowDirection, ForceMode.Force);
    }
    void RotationByCurrentDirection()
    {
        Quaternion targetRotation =
        Quaternion.Euler(0, Vector3.SignedAngle(Vector3.forward, WaterFlowDirection, Vector3.up), 0);
        transform.rotation =
        Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + WaterFlowDirection);
    }
}
