using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{

    void Start()
    {
        mixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume", 0));
        mixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume", 0));
        mixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume", 0));
        mixer.SetFloat("uiVolume", PlayerPrefs.GetFloat("uiVolume", 0));
        mixer.SetFloat("ambientVolume", PlayerPrefs.GetFloat("ambientVolume", 0));

    }

    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    float soundsVolumeMultiplier = 80;

    public void OnChangedMusicSlider(float value)
    {
        float volumeSetting = value; // 0...1
        float vol = volumeSetting == 0 ? -80 : (volumeSetting * (soundsVolumeMultiplier + 10)) - soundsVolumeMultiplier; // convert to DB
        mixer.SetFloat("musicVolume", vol);
        PlayerPrefs.SetFloat("musicVolume", vol);
    }
    public void OnChangedSFXSlider(float value)
    {
        float volumeSetting = value; // 0...1
        float vol = volumeSetting == 0 ? -80 : (volumeSetting * (soundsVolumeMultiplier + 10)) - soundsVolumeMultiplier; // convert to DB
        mixer.SetFloat("sfxVolume", vol);
        PlayerPrefs.SetFloat("sfxVolume", vol);
    }
    public void OnChangedMasterSlider(float value)
    {
        float volumeSetting = value; // 0...1
        float vol = volumeSetting == 0 ? -80 : (volumeSetting * (soundsVolumeMultiplier + 10)) - soundsVolumeMultiplier; // convert to DB
        mixer.SetFloat("masterVolume", vol);
        PlayerPrefs.SetFloat("masterVolume", vol);
    }
    public void OnChangedUISlider(float value)
    {
        float volumeSetting = value; // 0...1
        float vol = volumeSetting == 0 ? -80 : (volumeSetting * (soundsVolumeMultiplier + 10)) - soundsVolumeMultiplier; // convert to DB
        mixer.SetFloat("uiVolume", vol);
        PlayerPrefs.SetFloat("uiVolume", vol);
    }
    public void OnChangedAmbientSlider(float value)
    {
        float volumeSetting = value; // 0...1
        float vol = volumeSetting == 0 ? -80 : (volumeSetting * (soundsVolumeMultiplier + 10)) - soundsVolumeMultiplier; // convert to DB
        mixer.SetFloat("ambientVolume", vol);
        PlayerPrefs.SetFloat("ambientVolume", vol);
    }
}
