using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curveStat : MonoBehaviour
{
    public AnimationCurve curvePos, curveRot;

    void Update()
    {
        curvePos.AddKey(new Keyframe(Time.time, transform.position.z, 0, 0, 0, 0));
        curveRot.AddKey(new Keyframe(Time.time, transform.eulerAngles.y, 0, 0, 0, 0));
        //Нули в конструкторе ключа указываются для значений тангентов,
        //это делается, чтобы убрать сглаживание кривой по методу Безье
        //и лучше видеть ситуацию.
    }
}
