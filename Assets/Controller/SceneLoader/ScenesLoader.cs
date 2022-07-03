using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    bool isGamePlayScene = false;
    [SerializeField]
    int menuBGsceneIndex = 1,
    menuSceneIndex = 2;

    [SerializeField] Image progressBar;
    [SerializeField] GameObject endLoadingText;

    Animator fadeScreen;

    AsyncOperation sceneLoading, sceneLoading1;

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
        isGamePlayScene = true;
    }

    public void OnButtonExit()
    {
        if (isGamePlayScene)
            ExitToMenu();
        else
            ExitGame();
    }
    public void ExitToMenu()
    {
        sceneLoading = SceneManager.LoadSceneAsync(menuBGsceneIndex);
        sceneLoading.allowSceneActivation = false;

        //sceneLoading1.allowSceneActivation = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.anyKey || Input.touchCount > 0)
            if (sceneLoading != null)
            {
                sceneLoading.allowSceneActivation = true;
                sceneLoading1.allowSceneActivation = true;
            }



        if (progressBar != null && sceneLoading != null && sceneLoading1 != null)
        {
            progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount,
            ((sceneLoading.progress + sceneLoading1.progress)) / 0.9f, 0.05f);

            if (sceneLoading.progress / 0.9f > 0.9f && endLoadingText != null)
                endLoadingText.SetActive(true);
        }
    }

}

//[System.Serializable]
public enum SceneName
{
    wind = 3,
    paddle = 4,
    paddleUp = 5,
    sail = 6,
    sailUp = 7
}
