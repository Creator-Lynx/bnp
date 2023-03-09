using UnityEngine;
public abstract class SelfSaver : MonoBehaviour
{
    public virtual void Awake()
    {
        SavingManager.OnLoad.AddListener(Load);
        SavingManager.OnSave.AddListener(Save);
        Save();
    }
    protected abstract void Save();
    protected abstract void Load();
    public virtual void OnDestroy()
    {
        SavingManager.OnLoad.RemoveListener(Save);
        SavingManager.OnLoad.RemoveListener(Load);
    }

}