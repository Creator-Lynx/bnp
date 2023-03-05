using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSaver : SelfSaver
{
    [SerializeField]
    Transform playerFolower, cameraCurveFolower, springJointer, springTarget;
    Vector3 playerFol, curveFol, sJ, sT, cameraItselfPos;
    protected override void Load()
    {
        playerFolower.position = playerFol;
        cameraCurveFolower.position = curveFol;
        springJointer.position = sJ;
        springTarget.position = sT;
        transform.position = cameraItselfPos;
    }
    protected override void Save()
    {
        playerFol = playerFolower.position;
        curveFol = cameraCurveFolower.position;
        sJ = springJointer.position;
        sT = springTarget.position;
        cameraItselfPos = transform.position;
    }
}
