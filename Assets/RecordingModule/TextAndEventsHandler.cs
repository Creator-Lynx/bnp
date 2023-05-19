using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAndEventsHandler : MonoBehaviour
{
    [SerializeField] float timeToShowSimbol = 0.1f;
    [Space(30)]
    [TextArea(minLines: 4, maxLines: 8)]
    [SerializeField]
    UnityEvent[] Events;
    TextMeshProUGUI textComponent;
    AudioSource audioSource;

    int iterator = 0;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))

        {
            Events[iterator].Invoke();
        }
    }
    public IEnumerator TextShow(string text)
    {
        textComponent.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSecondsRealtime(timeToShowSimbol + UnityEngine.Random.Range(0f, 2f * timeToShowSimbol));
            textComponent.text = textComponent.text + text[i];
            audioSource.Play();
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }




}
