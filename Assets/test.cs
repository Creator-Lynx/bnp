using System.Diagnostics.Contracts;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    int framerate = 0;
    private void Start()
    {
        Application.targetFrameRate = framerate;
    }
    bool continued = false, once = false;
    int startFrame = 0;
    float startTime = 0f;
    private void Update()
    {


    }
    private void FixedUpdate()
    {
        Moving();
    }

    void Moving()
    {
        if (Time.unscaledTime >= 0.4f && !once)
        {
            continued = true;
            once = true;
            startFrame = Time.frameCount;
            startTime = Time.unscaledTime;
        }
        if (continued && Time.unscaledTime >= 1.4f)
        {
            continued = false;
            Debug.Log(Time.unscaledTime - startTime);
            Debug.Log(Time.frameCount - startFrame);
        }
        if (continued)
        {
            transform.position += Vector3.right * speed * Time.fixedUnscaledDeltaTime;
            Debug.Log("fixed " + Time.fixedUnscaledDeltaTime +
                    "\nunfixed" + Time.unscaledDeltaTime);
        }
    }
}
