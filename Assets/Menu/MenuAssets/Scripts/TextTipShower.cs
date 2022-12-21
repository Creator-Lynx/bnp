using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextTipShower : MonoBehaviour
{
    [SerializeField] float timeToShowSimbol = 0.1f;
    [Space(30)]
    [TextArea(minLines: 4, maxLines: 8)]
    [SerializeField]
    string[] texts;
    TextMeshProUGUI textComponent;
    AudioSource audioSource;

    int textNumber = 0;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator TextShow()
    {
        for (int i = 0; i < texts[textNumber].Length; i++)
        {
            yield return new WaitForSeconds(timeToShowSimbol + UnityEngine.Random.Range(0f, 2f * timeToShowSimbol));
            textComponent.text = textComponent.text + texts[textNumber][i];
            audioSource.Play();
        }

    }

    private void OnEnable()
    {
        textComponent.text = "";
        UnityEngine.Random.InitState((int)System.DateTime.Now.TimeOfDay.TotalSeconds);
        textNumber = UnityEngine.Random.Range(0, texts.Length);
        StartCoroutine(TextShow());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        textComponent.text = "";
    }
}
