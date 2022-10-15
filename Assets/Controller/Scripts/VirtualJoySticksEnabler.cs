using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoySticksEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_STANDALONE
        gameObject.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
