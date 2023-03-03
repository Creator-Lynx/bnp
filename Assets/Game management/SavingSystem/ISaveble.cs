using UnityEngine;
public abstract class ISaveble : MonoBehaviour
{
    private void Awake()
    {
        SavingSystem.OnLoad += Load;
        SavingSystem.OnSave += Save;
    }
    public abstract void Save();
    public abstract void Load();

    private void OnDestroy()
    {
        SavingSystem.OnLoad -= Load;
        SavingSystem.OnSave -= Save;
    }
}