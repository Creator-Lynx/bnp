using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetingsQualityByButton : MonoBehaviour
{
    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(5);
    }
    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(3);
    }
    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(1);
    }
}
