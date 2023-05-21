using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    Scrollbar masterSlider, musicSlider, sfxSlider, uiSlider, ambientSlider;
    void Start()
    {
        float[] data = new float[5];

        data[0] = PlayerPrefs.GetFloat("musicVolume", 0);
        musicSlider.value = ConvertToDB(data[0], false);
        mixer.SetFloat("musicVolume", data[0]);

        data[1] = PlayerPrefs.GetFloat("sfxVolume", 0);
        sfxSlider.value = ConvertToDB(data[1], false);
        mixer.SetFloat("sfxVolume", data[1]);

        data[2] = PlayerPrefs.GetFloat("masterVolume", 0);
        masterSlider.value = ConvertToDB(data[2], false);
        mixer.SetFloat("masterVolume", data[2]);

        data[3] = PlayerPrefs.GetFloat("uiVolume", 0);
        uiSlider.value = ConvertToDB(data[3], false);
        mixer.SetFloat("uiVolume", data[3]);

        data[4] = PlayerPrefs.GetFloat("ambientVolume", 0);
        ambientSlider.value = ConvertToDB(data[4], false);
        mixer.SetFloat("ambientVolume", data[4]);

    }

    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    float soundsVolumeMultiplier = 80;
    [SerializeField]
    float overZeroDBVolume = 5;

    float ConvertToDB(float value, bool isForward = true)
    {
        if (isForward)
            return value == 0 ? -80 : (value * (soundsVolumeMultiplier + overZeroDBVolume)) - soundsVolumeMultiplier; // convert to DB
        else return value == -80 ? 0 : (value + soundsVolumeMultiplier) / (soundsVolumeMultiplier + overZeroDBVolume); //convert to 0..1
    }
    public void OnChangedMusicSlider(float value)
    {
        float vol = ConvertToDB(value);
        mixer.SetFloat("musicVolume", vol);
        PlayerPrefs.SetFloat("musicVolume", vol);
    }
    public void OnChangedSFXSlider(float value)
    {
        float vol = ConvertToDB(value);
        mixer.SetFloat("sfxVolume", vol);
        PlayerPrefs.SetFloat("sfxVolume", vol);
    }
    public void OnChangedMasterSlider(float value)
    {
        float vol = ConvertToDB(value);
        mixer.SetFloat("masterVolume", vol);
        PlayerPrefs.SetFloat("masterVolume", vol);
    }
    public void OnChangedUISlider(float value)
    {
        float vol = ConvertToDB(value);
        mixer.SetFloat("uiVolume", vol);
        PlayerPrefs.SetFloat("uiVolume", vol);
    }
    public void OnChangedAmbientSlider(float value)
    {
        float vol = ConvertToDB(value);
        mixer.SetFloat("ambientVolume", vol);
        PlayerPrefs.SetFloat("ambientVolume", vol);
    }
}
