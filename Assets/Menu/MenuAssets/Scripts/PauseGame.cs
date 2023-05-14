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
        GetComponent<Button>().interactable = false;
#endif
        instance = this;
    }


    void Update()
    {
#if PLATFORM_STANDALONE
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
    public void ShowMenu()
    {
#if PLATFORM_STANDALONE
        Cursor.visible = true;
#endif
        IsMenuOpen = true;
        Pause();
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
}
