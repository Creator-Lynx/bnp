using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaterSimulation : MonoBehaviour
{
    [SerializeField] public float WaterDensity = 10f, ToFlowForceValue = 5f;

    [SerializeField] Vector3 forceDirection = Vector3.up;

    [SerializeField]
    float rig_drag = 2f, rig_angularDrag = 2f;
    public Vector3 WaterFlowDirection = Vector3.forward;

    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    public float divePercent;
    void FixedUpdate()
    {
        WaterFlowDirection = FlowCurveHandler.WaterVector;


        divePercent = -transform.position.y + 0.5f;
        divePercent = Mathf.Clamp(divePercent, 0f, 1f);

        rig.AddForce(forceDirection * divePercent * WaterDensity);
        rig.AddForce(FlowCurveHandler.ToFlowForce * ToFlowForceValue);
        rig.drag = divePercent * rig_drag;
        rig.angularDrag = divePercent * rig_angularDrag;
    }
}
