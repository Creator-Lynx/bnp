using UnityEngine;
public abstract class SelfSaver : MonoBehaviour
{
    public virtual void Awake()
    {
        SavingManager.OnLoad.AddListener(Load);
        SavingManager.OnSave.AddListener(Save);
    }
    protected abstract void Save();
    protected abstract void Load();

}