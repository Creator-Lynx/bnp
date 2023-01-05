using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    BoatMover mover;
    void Start()
    {
        mover = GetComponent<BoatMover>();
    }

    void Update()
    {
        mover.horizontal = Input.GetAxis("Horizontal");
    }

}
