using System.Collections;
using UnityEngine;

public class BasicGameManager : MonoBehaviour
{
    [SerializeField]
    Animator loadScreen, endScreen;
    [SerializeField]
    Animation grayScreenPP;
    public static BasicGameManager instance;
    [SerializeField]
    AnimationCurve slowingTimeModificator;

    [SerializeField]
    float slowTime = 1f;
    private void Start()
    {
        instance = this;
        Animator[] animators = GameObject.FindGameObjectWithTag("Menu")?.GetComponentsInChildren<Animator>();
        if (animators != null)
            for (int i = 0; i < animators.Length; i++)
            {
                if (animators[i]?.name == "LoadScreen") loadScreen = animators?[i];
                if (animators[i]?.name == "WinAndLose") endScreen = animators?[i];
            }
#if PLATFORM_STANDALONE
        Cursor.visible = false;
#endif

    }
    public static void CompleteLevel()
    {
        Debug.Log("WIn");
        instance.CallMenu(true);
        instance.StartCoroutine(instance.SlowTime(instance.slowTime));
#if PLATFORM_STANDALONE
        Cursor.visible = true;
#endif
    }
    public static void LoseLevel()
    {
        Debug.Log("Lose");
        instance.StartCor();
#if PLATFORM_STANDALONE
        Cursor.visible = true;
#endif
    }
    void StartCor()
    {
        StartCoroutine(DeathWait(7f));
    }
    void CallMenu(bool isWin)
    {
        if (!loadScreen || !endScreen)
        {
            Debug.LogWarning("Menus does not exists on scene.");
            return;
        }
        //loadScreen.SetTrigger("Hide");
        if (isWin) endScreen.SetTrigger("win");
        else endScreen.SetTrigger("lose");
    }

    IEnumerator DeathWait(float waitSec)
    {
        grayScreenPP.Play();
        yield return new WaitForSeconds(waitSec - 0.5f * slowTime);
        StartCoroutine(SlowTime(slowTime));
        yield return new WaitForSecondsRealtime(0.5f * slowTime);
        CallMenu(false);

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
        //for (int i = 0; i < 50; i++)
        //{
        //    t += sec / 50f;
        //    Time.timeScale = Mathf.InverseLerp(sec, 0, t);
        //    yield return new WaitForSeconds(sec / 50f);
        //}
    }

}
