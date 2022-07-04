using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField]
    bool isSceneLoading = false;
    [SerializeField]
    int menuBGSceneIndex = 1,
    menuSceneIndex = 2;

    static int currentActiveScene = 1;

    [SerializeField]
    Image progressBar;
    [SerializeField]
    GameObject endLoadingText;
    [SerializeField]
    Animator fadeScreen;

    AsyncOperation sceneLoading, sceneUnloading;


    void Start()
    {
        currentActiveScene = 1;
    }
    public void LoadScene(int sceneID)
    {
        sceneUnloading = SceneManager.UnloadSceneAsync(currentActiveScene);
        //sceneUnloading.allowSceneActivation = false;
        currentActiveScene = sceneID;
        sceneLoading = SceneManager.LoadSceneAsync(currentActiveScene, LoadSceneMode.Additive);
        sceneLoading.allowSceneActivation = false;
        fadeScreen.SetTrigger("Hide");
        progressBar.fillAmount = 0;
        isSceneLoading = true;
    }

    public void OnButtonExit()
    {
        if (currentActiveScene != menuBGSceneIndex)
            ExitToMenu();
        else
            ExitGame();
    }
    public void ExitToMenu()
    {
        currentActiveScene = menuBGSceneIndex;
        SceneManager.LoadScene(menuBGSceneIndex);
        SceneManager.LoadScene(menuSceneIndex, LoadSceneMode.Additive);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (isSceneLoading)
        {
            if (progressBar.gameObject.activeSelf)
            {
                if (progressBar != null && sceneLoading != null)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount,
                    (sceneLoading.progress) / 0.9f, 0.05f);

                    if (sceneLoading.progress / 0.9f > 0.9f && endLoadingText != null)
                    {
                        endLoadingText.SetActive(true);
                    }

                }
            }


            if (Input.anyKey || Input.touchCount > 0)
                if (sceneLoading != null)
                {
                    sceneLoading.allowSceneActivation = true;
                }

            if (sceneLoading.isDone)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
                isSceneLoading = false;
                fadeScreen.SetTrigger("Show");
                endLoadingText.SetActive(false);
            }
        }


    }

}

