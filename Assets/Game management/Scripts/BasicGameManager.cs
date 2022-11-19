using System.Collections;
using UnityEngine;

public class BasicGameManager : MonoBehaviour
{
    [SerializeField]
    Animator loadScreen, endScreen;
    public static BasicGameManager instance;

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
        yield return new WaitForSeconds(waitSec);
        CallMenu(false);

    }

    IEnumerator SlowTime(float sec)
    {
        float t = 0;
        for (int i = 0; i < 50; i++)
        {
            Time.timeScale = Mathf.InverseLerp(sec, 0, t);
            t += sec / 50f;
            yield return new WaitForSeconds(sec / 50f);
        }
    }

}
