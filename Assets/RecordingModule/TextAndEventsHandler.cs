using System;
using System.Collections;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextAndEventsHandler : MonoBehaviour
{
    [SerializeField] float timeToShowSimbol = 0.1f;
    [Space(30)]

    [SerializeField]
    UnityEvent[] Events;
    TextMeshProUGUI textComponent;
    AudioSource audioSource;

    static int iterator = 0;
    static TextAndEventsHandler instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject.transform.parent.parent.gameObject);
            return;
        }
        iterator = 0;
        DontDestroyOnLoad(gameObject.transform.parent.parent);
        instance = this;
        textComponent = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Events[iterator++].Invoke();
    }
    public void ShowText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(TextShow(text));
    }
    public IEnumerator TextShow(string text)
    {
        textComponent.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(timeToShowSimbol + UnityEngine.Random.Range(0f, 2f * timeToShowSimbol));
            textComponent.text = textComponent.text + text[i];
            audioSource.Play();
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void PlayAnimation(Animation animated)
    {
        animated.Play();
    }

    public void StartFadeMusic(AudioSource source)
    {
        StartCoroutine(FadeMusic(source, 1.5f));
    }
    public IEnumerator FadeMusic(AudioSource source, float time)
    {
        float timer = time;
        while (timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            source.pitch = Mathf.Lerp(0.8f, 0, (-timer + time) / time);
            source.volume = Mathf.Lerp(1f, 0, (-timer + time) / time);
        }
    }

    public void MuteMusic(AudioSource source)
    {
        StartCoroutine(MuteMusicCor(source, 1.5f));
    }
    public IEnumerator MuteMusicCor(AudioSource source, float time)
    {
        float timer = time;
        while (timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            source.volume = Mathf.Lerp(1f, 0.4f, (-timer + time) / time);
        }
    }
    public void UnMuteMusic(AudioSource source)
    {
        StartCoroutine(UnMuteMusicCor(source, 1.5f));
    }
    public IEnumerator UnMuteMusicCor(AudioSource source, float time)
    {
        float timer = time;
        while (timer > 0)
        {
            yield return new WaitForEndOfFrame();
            timer -= Time.deltaTime;
            source.volume = Mathf.Lerp(0.4f, 1, (-timer + time) / time);
        }
    }

}
