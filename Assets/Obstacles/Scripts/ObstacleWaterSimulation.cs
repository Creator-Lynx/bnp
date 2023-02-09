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
    void FixedUpdate()
    {
        WaterFlowDirection = _curveHandler.WaterVector;


        divePercent = -transform.position.y + 0.5f;
        divePercent = Mathf.Clamp(divePercent, 0f, 1f);

        rig.AddForce(forceDirection * divePercent * WaterDensity);
        rig.AddForce(FlowCurveHandler.ToFlowForce * ToFlowForceValue);
        rig.drag = divePercent * rig_drag;
        rig.angularDrag = divePercent * rig_angularDrag;
        if (divePercent > 0.25f)
            ForceMoveByCurrentDirection();
        RotationByCurrentDirection();
    }


    [SerializeField] float flowForce = 5f, rotateSpeed = 0.5f;

    Vector3 moveByFlowDirection;

    void ForceMoveByCurrentDirection()
    {
        rig.AddForce(flowForce * WaterFlowDirection, ForceMode.Force);
    }
    void RotationByCurrentDirection()
    {
        Quaternion targetRotation = Quaternion.Euler(0, Mathf.Asin(WaterFlowDirection.normalized.x) * Mathf.Rad2Deg, 0);
        transform.rotation =
        Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * 0.1f * (Time.fixedDeltaTime / Time.fixedUnscaledDeltaTime));
    }
}
