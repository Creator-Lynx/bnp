using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButtonHider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
#if PLATFORM_STANDALONE
        GetComponent<Button>().enabled = false;
        GetComponent<Image>().enabled = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = !Cursor.visible;
            OnButtonESCdown.Invoke();
        }

    }
    public void HideMenu()
    {
        Cursor.visible = false;
    }
    public void ShowMenu()
    {
        
    }
    [SerializeField]
    UnityEvent OnButtonESCdown;

}
