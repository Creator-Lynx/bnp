using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Setings : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown targetFPSDropdown;
    [SerializeField]
    Toggle vsyncToggle;
    private void Start()
    {
        //quality level
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QUALITY_LEVEL", 0));
        //target framerate
        int code = PlayerPrefs.GetInt("TARGET_FPS", 2);
        SetTargetFrameRate(code);
        targetFPSDropdown.value = code;
        //vsync
        code = PlayerPrefs.GetInt("VSYNC", 0);
        QualitySettings.vSyncCount = code;
        vsyncToggle.isOn = code == 1;
    }
    public void SetHighQuality()
    {
        QualitySettings.SetQualityLevel(2);
        PlayerPrefs.SetInt("QUALITY_LEVEL", 2);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSYNC", 0);
    }
    public void SetMediumQuality()
    {
        QualitySettings.SetQualityLevel(1);
        PlayerPrefs.SetInt("QUALITY_LEVEL", 1);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSYNC", 0);
    }
    public void SetLowQuality()
    {
        QualitySettings.SetQualityLevel(0);
        PlayerPrefs.SetInt("QUALITY_LEVEL", 0);
        QualitySettings.vSyncCount = PlayerPrefs.GetInt("VSYNC", 0);
    }
    public void SetTargetFrameRate(int code)
    {
        switch (code)
        {
            case 0:
                Application.targetFrameRate = 1000;
                break;
            case 1:
                Application.targetFrameRate = 30;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
            case 4:
                Application.targetFrameRate = 144;
                break;
            default:
                Application.targetFrameRate = 1000;
                break;
        }
        PlayerPrefs.SetInt("TARGET_FPS", code);
    }
    public void VSyncToggle(bool enabled)
    {
        if (enabled)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("VSYNC", 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("VSYNC", 0);
        }

    }
}
