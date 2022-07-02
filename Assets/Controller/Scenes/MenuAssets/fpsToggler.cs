using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsToggler : MonoBehaviour
{
    FPSCounter counter;
    void Start()
    {
        counter = FindObjectOfType<FPSCounter>();
    }

    public void ToggleFPSCounter(bool toggle)
    {
        if (counter != null)
            counter.gameObject.SetActive(toggle);
        Debug.Log(toggle);
    }

}
