using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextShower : MonoBehaviour
{
    [SerializeField] float timeToShowSimbol = 0.1f;
    [TextArea(minLines: 4, maxLines: 8)]
    [SerializeField] string text = "Данный тестовый билд игры является способом исследования различных вариантов управления и поска наиболее интересного посредством выбора одного из вариантов или комбинации оных. \n Я также собираю информацию о ваших выборах и проведенном времени на уровнях для образования выводов.";
    [SerializeField] string sceneNameToLoad = "", sceneNameToLoad1 = "";
    TextMeshProUGUI textComponent;
    AudioSource audioSource;
    [SerializeField] Image progressBar;
    [SerializeField] GameObject endLoadingText;
    AsyncOperation sceneLoading, sceneLoading1;
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TextShow());
        sceneLoading = SceneManager.LoadSceneAsync(sceneNameToLoad);
        sceneLoading.allowSceneActivation = false;
        sceneLoading1 = SceneManager.LoadSceneAsync(sceneNameToLoad1, LoadSceneMode.Additive);
        sceneLoading1.allowSceneActivation = false;
    }

    IEnumerator TextShow()
    {
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(timeToShowSimbol + UnityEngine.Random.Range(0, timeToShowSimbol));
            textComponent.text = textComponent.text + text[i];
            audioSource.Play();
        }

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
            Debug.Log("load 0 " + sceneLoading.progress);
            Debug.Log("load 1 " + sceneLoading1.progress);
            Debug.Log("load all " + sceneLoading.progress + sceneLoading1.progress);
            if (sceneLoading.progress / 0.9f > 0.9f && sceneLoading1.progress / 0.9f > 0.9f && endLoadingText != null)
                endLoadingText.SetActive(true);
        }




    }
}
