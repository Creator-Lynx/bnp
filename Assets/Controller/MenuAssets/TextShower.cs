using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextShower : MonoBehaviour
{
    [SerializeField] float timeToShowSimbol = 0.1f;
    [TextArea(minLines: 4, maxLines: 8)]
    [SerializeField] string text = "Данный тестовый билд игры является способом исследования различных вариантов управления и поска наиболее интересного посредством выбора одного из вариантов или комбинации оных. \n Я также собираю информацию о ваших выборах и проведенном времени на уровнях для образования выводов.";

    TextMeshProUGUI textComponent;
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TextShow());
    }

    IEnumerator TextShow()
    {
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(timeToShowSimbol);
            textComponent.text = textComponent.text + text[i];

        }



    }
}
