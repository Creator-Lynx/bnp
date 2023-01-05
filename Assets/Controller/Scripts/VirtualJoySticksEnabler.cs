using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoySticksEnabler : MonoBehaviour
{

    void Start()
    {
#if PLATFORM_STANDALONE
        gameObject.SetActive(false);
#endif
    }
}
