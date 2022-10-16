using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image selfImage;
    [SerializeField]
    float timeToChange = 0.4f;
    private void Start()
    {
        selfImage = GetComponent<Image>();
    }
    float currentHP = 1;
    IEnumerator BarAnimation()
    {
        float timer = 0;
        while (timer < timeToChange)
        {
            selfImage.fillAmount = Mathf.Lerp(selfImage.fillAmount, currentHP, timer / timeToChange);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }

    /// <summary>
    /// hp ranged from 0 to 1.
    /// </summary>
    /// <param name="hp"></param>
    public void OnHpChange(float hp)
    {
        currentHP = hp;
        StartCoroutine(BarAnimation());
    }

}
