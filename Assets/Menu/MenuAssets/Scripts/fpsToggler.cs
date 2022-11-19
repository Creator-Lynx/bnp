using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fpsToggler : MonoBehaviour
{
    public static bool isPP = true;
    FPSCounter counter;
    [SerializeField]
    Toggle ppToggle;

    private void Awake()
    {
        isPP = PlayerPrefs.GetInt("IsPP", 1) == 1;
        counter = FindObjectOfType<FPSCounter>();
    }
    void Start()
    {
        if (counter != null) counter.gameObject.SetActive(false);
        if (ppToggle != null) ppToggle.isOn = isPP;
    }

    public void ToggleFPSCounter(bool toggle)
    {
        if (counter != null)
            counter.gameObject.SetActive(toggle);
    }

    public void TogglePostProcessing(bool toggle)
    {
        if (ppEnabler.instance != null)
            ppEnabler.instance.TurnPPEnabled(toggle);
        isPP = toggle;
        PlayerPrefs.SetInt("IsPP", toggle ? 1 : 0);



    }
}

