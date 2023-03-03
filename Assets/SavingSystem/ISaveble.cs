using UnityEngine;
public abstract class ISaveble : MonoBehaviour
{
    private void Start()
    {
        //подписка
    }
    public abstract void Save();
    public abstract void Load();
}