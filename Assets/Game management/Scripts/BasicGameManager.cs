using System.Collections;
using UnityEngine;

public class BasicGameManager : MonoBehaviour
{
    [SerializeField]
    Animator loadScreen, endScreen;
    public static BasicGameManager instance;
    private void Start()
    {
        instance = this;
        Animator[] animators = GameObject.FindGameObjectWithTag("Menu")?.GetComponentsInChildren<Animator>();
        for (int i = 0; i < animators.Length; i++)
        {
            if (animators[i]?.name == "LoadScreen") loadScreen = animators?[i];
            if (animators[i]?.name == "WinAndLose") endScreen = animators?[i];
        }


    }
    public static void CompleteLevel()
    {
        Debug.Log("WIn");
        instance.CallMenu(true);
    }
    public static void LoseLevel()
    {
        Debug.Log("Lose");
        instance.StartCor();
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
        loadScreen.SetTrigger("Hide");
        if (isWin) endScreen.SetTrigger("win");
        else endScreen.SetTrigger("lose");
    }

    IEnumerator DeathWait(float waitSec)
    {
        yield return new WaitForSeconds(waitSec);
        CallMenu(false);

    }
}
