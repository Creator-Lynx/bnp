using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ppEnabler : MonoBehaviour
{
    public static ppEnabler instance;
    PostProcessLayer layer;
    void Start()
    {
        instance = this;
        layer = GetComponent<PostProcessLayer>();
        TurnPPEnabled(fpsToggler.isPP);
    }

    public void TurnPPEnabled(bool enabled)
    {
        layer.enabled = enabled;
    }

}
