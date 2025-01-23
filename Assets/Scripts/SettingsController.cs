using System;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public event Action OnSettingsLoaded;

    [SerializeField] private AudioMixer audioMixer;
    public SettingsData settingsData;

    private void Start()
    {
        LoadSettings();
    }

    public void SetMusicVolume(float volume)
    {
        if (volume < 0.0001f) volume = 0.0001f;
        settingsData.musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        settingsData.graphicsQuality = (GraphicsQuality)qualityIndex;
        QualitySettings.SetQualityLevel((int)qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", (int)qualityIndex);
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            settingsData.musicVolume = volume;
            audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        }
        if (PlayerPrefs.HasKey("GraphicsQuality"))
        {
            int quality = PlayerPrefs.GetInt("GraphicsQuality");
            settingsData.graphicsQuality = (GraphicsQuality)quality;
            QualitySettings.SetQualityLevel(quality);
        }
        OnSettingsLoaded?.Invoke();
    }
}