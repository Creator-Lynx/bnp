using UnityEngine;

public class CameraSaver : SelfSaver
{
    [SerializeField]
    Transform playerFolower, cameraCurveFolower, springJointer, springTarget;
    Vector3 playerFol, curveFol, sJ, sT, cameraItselfPos;
    Quaternion cameraRotation;
    float _t;
    protected override void Load()
    {
        playerFolower.position = playerFol;
        cameraCurveFolower.position = curveFol;
        springJointer.position = sJ;
        springTarget.position = sT;
        transform.position = cameraItselfPos;
        cameraCurveFolower.GetComponent<CameraCurveMover>().t = _t;
        transform.rotation = cameraRotation;
    }
    protected override void Save()
    {
        playerFol = playerFolower.position;
        curveFol = cameraCurveFolower.position;
        sJ = springJointer.position;
        sT = springTarget.position;
        cameraItselfPos = transform.position;
        _t = cameraCurveFolower.GetComponent<CameraCurveMover>().t;
        cameraRotation = transform.rotation;
    }
}
