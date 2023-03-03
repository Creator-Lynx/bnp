using System;
using UnityEngine;

public class SavingSystem : MonoBehaviour
{
    private void Start()
    {
        MakeSave();
    }
    public static void MakeSave()
    {
        OnSave.Invoke();
    }
    public static void MakeLoad()
    {
        OnLoad.Invoke();
    }
    public static Action OnLoad, OnSave;
}
