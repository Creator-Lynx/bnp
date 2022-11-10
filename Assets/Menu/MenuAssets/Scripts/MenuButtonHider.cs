using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonHider : MonoBehaviour
{
    [SerializeField]
    public bool IsMenuOpen = true;

    void Start()
    {
#if PLATFORM_STANDALONE
        GetComponent<Button>().interactable = false;
#endif
    }

    // Update is called once per frame
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
    }
    public void ShowMenu()
    {
#if PLATFORM_STANDALONE
        Cursor.visible = true;
#endif
        IsMenuOpen = true;
    }
    [SerializeField]
    UnityEvent OnMenuShowing, OnMenuHidding;

}
