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

    [Header("Debugging wind force value")]
    [SerializeField]
    float windForceValue;

    Vector3 tempVector = Vector3.zero;
    void Update()
    {
        transform.rotation = windDirectionObject.rotation;
        tempVector.z = -soundSourceDistanceRange * Mathf.InverseLerp(maxWindForce, minWindForce, SailMover.GlobalFinalWindForce);
        windForceValue = SailMover.GlobalFinalWindForce;
        soundSourceObject.localPosition = tempVector;
    }
}
