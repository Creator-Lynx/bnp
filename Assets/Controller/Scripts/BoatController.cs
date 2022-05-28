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

    // Update is called once per frame
    void Update()
    {
        mover.horizontal = Input.GetAxis("Horizontal");
    }

}
