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
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.O)) MakeSave();
        if (Input.GetKey(KeyCode.L)) MakeLoad();
#endif
    }
    public static void MakeSave()
    {
        Debug.Log("MakeSave");
        OnSave.Invoke();
    }
    public static void MakeLoad()
    {
        Debug.Log("MakeLoad");
        OnLoad.Invoke();
    }
    public static UnityEvent OnLoad = new UnityEvent(), OnSave = new UnityEvent();
}
