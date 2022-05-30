using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControlable
{

    void Move(Vector3 direction);
    void RotateY(float degree);
}
