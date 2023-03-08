using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    GameObject endLoadingText, tipText;
    [SerializeField]
    AudioListener localCameraListener;
    [SerializeField]
    MenuButtonHider menuButton;
    [SerializeField]
    Animator fadeScreen, canvasMenu;
    static ScenesLoader instance;
    public static Animator FadeScreen
    {
        get { return instance?.fadeScreen; }
    }

    AsyncOperation sceneLoading, sceneUnloading;


    void Start()
    {
        instance = this;
        currentActiveScene = menuBGSceneIndex;
    }
    public void LoadScene(int sceneID)
    {

#if UNITY_EDITOR
#else
        AnalyticsProtoSendler.SendAnalitics(
            sceneID, SceneManager.GetSceneByBuildIndex(sceneID).name,
            currentActiveScene, SceneManager.GetSceneByBuildIndex(currentActiveScene).name
        );
#endif
        BasicGameManager.StopCorutines();
        sceneUnloading = SceneManager.UnloadSceneAsync(currentActiveScene);
        //sceneUnloading.allowSceneActivation = false;
        currentActiveScene = sceneID;
        sceneLoading = SceneManager.LoadSceneAsync(currentActiveScene, LoadSceneMode.Additive);
        sceneLoading.allowSceneActivation = false;
        fadeScreen.SetTrigger("Hide");
        canvasMenu.SetTrigger("Inactive");
        menuButton.IsMenuOpen = false;
        progressBar.fillAmount = 0;
        tipText.SetActive(true);
        fixedTouchOneFrameDelay = false;
        isSceneLoading = true;
        localCameraListener.enabled = true;
        Time.timeScale = 1;
    }

    public void OnButtonExit()
    {

        if (currentActiveScene != menuBGSceneIndex)
        {
            ExitToMenu();
        }
        else
        {
            //Debug.Log("Exit");
            ExitGame();
        }
        Time.timeScale = 1;

    }
    public void ExitToMenu()
    {
#if UNITY_EDITOR
#else
        AnalyticsProtoSendler.SendAnalitics(
           menuBGSceneIndex, SceneManager.GetSceneByBuildIndex(menuBGSceneIndex).name,
           currentActiveScene, SceneManager.GetSceneByBuildIndex(currentActiveScene).name
       );
#endif
        currentActiveScene = menuBGSceneIndex;
        SceneManager.LoadScene(menuBGSceneIndex);
        SceneManager.LoadScene(menuSceneIndex, LoadSceneMode.Additive);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    bool fixedTouchOneFrameDelay = false;
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


            if (Input.anyKey || (Input.touchCount > 0 && fixedTouchOneFrameDelay))
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
                tipText.SetActive(false);
                progressBar.fillAmount = 0;
                localCameraListener.enabled = false;
            }
            fixedTouchOneFrameDelay = true;
        }


    }

}

