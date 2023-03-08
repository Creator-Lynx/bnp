using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CheckPointReturn : MonoBehaviour
{
    [SerializeField]
    float slowingTime = 1,
    unslowingTime = 1f,
    graingTime = 1f,
    ungraingTime = 1f;
    [SerializeField]
    PostProcessVolume grayScreenVolume;
    public void StartReturn()
    {
        StartCoroutine(ReturnToCheckPoint());
    }
    IEnumerator ReturnToCheckPoint()
    {
        //slow time
        StartCoroutine(SlowTime(slowingTime));
        //start gray screen     

        //fade in

        //loading
        SavingManager.MakeLoad();
        //fade out

        //normalize time
        StartCoroutine(NormalizeTime(unslowingTime));
        //normalise grey screen
        yield return new WaitForSeconds(1f);
    }
    IEnumerator SlowTime(float sec)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.InverseLerp(sec, 0, t);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator NormalizeTime(float sec)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.InverseLerp(0, sec, t);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator GraingScreen(float sec)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.unscaledDeltaTime;
            grayScreenVolume.weight = Mathf.InverseLerp(0, sec, t);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator UngraingScreen(float sec)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.unscaledDeltaTime;
            grayScreenVolume.weight = Mathf.InverseLerp(0, sec, t);
            yield return new WaitForEndOfFrame();
        }
    }
}
