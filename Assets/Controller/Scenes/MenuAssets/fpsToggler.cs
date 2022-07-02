using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsToggler : MonoBehaviour
{
    FPSCounter counter;
    private void Awake()
    {
        counter = FindObjectOfType<FPSCounter>();
    }
    void Start()
    {
        if (counter != null) counter.gameObject.SetActive(false);
    }

    public void ToggleFPSCounter(bool toggle)
    {
        if (counter != null)
            counter.gameObject.SetActive(toggle);
    }

}
