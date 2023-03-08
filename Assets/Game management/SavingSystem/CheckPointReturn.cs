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
        StartCoroutine(GraingScreen(graingTime));
        //vibraning
#if !PLATFORM_STANDALONE
        StartCoroutine(Vibrate());
#endif
        //fade in
        yield return new WaitForSecondsRealtime(slowingTime - 0.1f);
        ScenesLoader.FadeScreen?.SetTrigger("Hide");
        //loading
        yield return new WaitForSecondsRealtime(0.2f);
        SavingManager.MakeLoad();
        //fade out
        yield return new WaitForSecondsRealtime(0.05f);
        ScenesLoader.FadeScreen?.SetTrigger("Show");
        //normalize time
        StartCoroutine(NormalizeTime(unslowingTime));
        //normalise grey screen
        StartCoroutine(UngraingScreen(ungraingTime));
        yield return new WaitForSeconds(1f);
    }
    IEnumerator SlowTime(float sec)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.InverseLerp(sec, -0.2f, t);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator NormalizeTime(float sec)
    {
        float t = 0;
        while (t < sec)
        {
            t += Time.unscaledDeltaTime;
            Time.timeScale = Mathf.InverseLerp(-0.2f, sec, t);
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
            grayScreenVolume.weight = Mathf.InverseLerp(sec, 0, t);
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator Vibrate()
    {
#if !PLATFORM_STANDALONE
        Handheld.Vibrate();
        yield return new WaitForSeconds(0.3f);
        Handheld.Vibrate();
#endif
        yield return new WaitForEndOfFrame();
    }
}
