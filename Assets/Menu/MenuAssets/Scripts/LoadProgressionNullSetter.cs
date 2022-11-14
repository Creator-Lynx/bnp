using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadProgressionNullSetter : MonoBehaviour
{
    [SerializeField]
    Image loadProgress;
    public void SetLoadProgressUIToNull()
    {
        loadProgress.fillAmount = 0;
    }
}
