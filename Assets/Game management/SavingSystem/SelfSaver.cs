using UnityEngine;
public abstract class SelfSaver : MonoBehaviour
{
    public virtual void Awake()
    {
        //Debug.Log("SelfSaver Awaked" + gameObject.name);
        SavingManager.OnLoad.AddListener(Load);
        SavingManager.OnSave.AddListener(Save);
    }
    protected abstract void Save();
    protected abstract void Load();

}