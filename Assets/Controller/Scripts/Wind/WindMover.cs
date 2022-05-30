using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMover : MonoBehaviour
{
    enum conType
    {
        characterController
    }
    [SerializeField]
    conType type;
    IControlable controller;
    // Start is called before the first frame update
    void Start()
    {
        if (type == 0)
            controller = GetComponent<CharacterControllBehavior>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
