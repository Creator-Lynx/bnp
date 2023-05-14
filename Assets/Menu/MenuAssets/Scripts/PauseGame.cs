using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    public bool IsMenuOpen = true;
    public static PauseGame instance;
    void Start()
    {
#if PLATFORM_STANDALONE
        //GetComponent<Button>().interactable = false;
        DeactivateVisualUI();
#endif
        instance = this;
    }

    void DeactivateVisualUI()
    {
        GetComponent<Button>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<Image>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
#if PLATFORM_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape) & !IsChangingDelayed)
        {
            if (IsMenuOpen)
            {
                HideMenu();
                OnMenuHidding.Invoke();
            }
            else
            {
                ShowMenu();
                OnMenuShowing.Invoke();
            }

        }
#endif
    }
    public void HideMenu()
    {
#if PLATFORM_STANDALONE
        Cursor.visible = false;
#endif
        IsMenuOpen = false;
        ContinueGame();
        StartCoroutine(MenuChangingDelay());
    }
    public void ShowMenu()
    {
#if PLATFORM_STANDALONE
        Cursor.visible = true;
#endif
        IsMenuOpen = true;
        Pause();
        StartCoroutine(MenuChangingDelay());
    }
    public UnityEvent OnMenuShowing, OnMenuHidding;
    public void Pause()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
            Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    bool IsChangingDelayed = false;
    IEnumerator MenuChangingDelay()
    {
        GetComponent<Button>().interactable = false;
        IsChangingDelayed = true;
        yield return new WaitForSecondsRealtime(0.5f);
        GetComponent<Button>().interactable = true;
        IsChangingDelayed = false;
    }
}
