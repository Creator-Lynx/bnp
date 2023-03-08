using UnityEngine;
using UnityEngine.Events;

public class SavingManager : MonoBehaviour
{
    private void Start()
    {
        MakeSave();
    }
    private void Update()
    {
#if UNITY_EDITOR_64
        if (Input.GetKey(KeyCode.O)) MakeSave();
        if (Input.GetKey(KeyCode.L)) MakeLoad();
#endif
    }
    public static void MakeSave()
    {
        Debug.Log("Save");
        OnSave.Invoke();
    }
    public static void MakeLoad()
    {
        Debug.Log("Load");
        OnLoad.Invoke();
    }
    public static UnityEvent OnLoad = new UnityEvent(), OnSave = new UnityEvent();
}
