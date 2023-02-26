using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraFOVcontroller : MonoBehaviour
{
    [SerializeField]
    float minSqrDelta, maxSqrDelta;
    [Space(20)]
    [SerializeField]
    float minFOV, maxFOV, FOVLerpSpeed = 1f;
    Vector3 prevPosition;
    Vector3 prevForward;
    [SerializeField]
    Transform target;
    [Space(40)] public float testSqrDelta;
    Camera selfCam;
    void Start()
    {
        selfCam = GetComponent<Camera>();
        prevPosition = target.position;
        prevForward = target.forward;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaV = target.position - prevPosition;
        deltaV = Vector3.Project(deltaV, prevForward);

        prevPosition = target.position;
        prevForward = target.forward;

        float sqrDelta = deltaV.sqrMagnitude;
        testSqrDelta = sqrDelta;
        float t = Mathf.InverseLerp(minSqrDelta, maxSqrDelta, sqrDelta);
        float targetFOV = Mathf.Lerp(minFOV, maxFOV, t);
        selfCam.fieldOfView = Mathf.Lerp(selfCam.fieldOfView, targetFOV, FOVLerpSpeed * Time.deltaTime);
    }
}
