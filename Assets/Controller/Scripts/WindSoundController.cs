using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSoundController : MonoBehaviour
{
    [Header("Referencing and controlled objects")]
    [SerializeField]
    Transform windDirectionObject;
    [SerializeField]
    Transform soundSourceObject;
    AudioLowPassFilter sourceLowPass;
    Transform cameraTransform;
    [Header("Controll distance of sound source")]
    [Space(30)]
    [Range(0, 2)]
    [SerializeField]
    float minWindForce = 0;
    [Range(2, 15)]
    [SerializeField]
    float maxWindForce = 10;
    [Range(0, 5)]
    [SerializeField]
    float soundSourceDistanceRange = 2;

    [Header("Debugging")]
    [Space(10)]
    [SerializeField]
    float windForceValue;
    [SerializeField]
    bool enableRotationByWind = true;


    [Header("Simulate rear sound")]
    [Space(30)]
    [SerializeField]
    [Range(10, 22000)]
    float basicLowPassCutoff = 2000;
    [SerializeField]
    [Range(10, 5000)]
    float maximumLowPassCutoff = 2000;

    [Header("Remaping")]
    [SerializeField]
    bool enableRemaping = false;
    [SerializeField]
    AnimationCurve remapThreasholdInterpolaitonCurve;


    void Start()
    {
        sourceLowPass = GetComponentInChildren<AudioLowPassFilter>();
        cameraTransform = Camera.main.transform;
    }

    Vector3 tempVector = Vector3.zero;
    void Update()
    {
        if (enableRotationByWind)
            transform.rotation = windDirectionObject.rotation;
        tempVector.z = -soundSourceDistanceRange * Mathf.InverseLerp(maxWindForce, minWindForce, SailMover.GlobalFinalWindForce) + 0.2f;
        windForceValue = SailMover.GlobalFinalWindForce;
        soundSourceObject.localPosition = tempVector;

        //LOW PASS ============================

        //obtain the angle of position. how we need to increas lowpass.
        Vector3 horCameraVector = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up);
        float scalar = Vector3.Dot(transform.forward, horCameraVector);
        float t = scalar > 0 ? scalar : 0;


        //applying frequency cutoff
        float remapedT = enableRemaping ? remapThreasholdInterpolaitonCurve.Evaluate(t) : t;
        sourceLowPass.cutoffFrequency = Mathf.Lerp(basicLowPassCutoff, maximumLowPassCutoff, remapedT);


    }
}
