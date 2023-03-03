using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSaver : ISaveble
{
    [SerializeField]
    Transform playerFolower, cameraCurveFolower, springJointer, springTarget;
    Vector3 playerFol, curveFol, sJ, sT, cameraItselfPos;
    public override void Load()
    {

    }
    public override void Save()
    {
        playerFol = playerFolower.position;
        curveFol = cameraCurveFolower.position;
        sJ = springJointer.position;
        sT = springTarget.position;
        cameraItselfPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
