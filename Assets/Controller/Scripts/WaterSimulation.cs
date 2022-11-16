using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WaterSimulation : MonoBehaviour
{
    [SerializeField] public float WaterDensity = 10f;

    [SerializeField] Vector3 forceDirection = Vector3.up;

    public Vector3 WaterFlowDirection = Vector3.forward;

    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        WaterFlowDirection = FlowCurveHandler.WaterVector;


        float divePercent = -transform.position.y + transform.localScale.x * 0.5f;
        divePercent = Mathf.Clamp(divePercent, 0f, 1f);


        rig.AddForce(forceDirection * divePercent * WaterDensity * (Time.fixedDeltaTime / 0.02f));
        rig.drag = divePercent * 2f;
        rig.angularDrag = divePercent * 2f;
    }
}
