using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    public SettingsData settingsData;
    public AudioMixer audioMixer;  // Для управления громкостью музыки

    private void Start()
    {
        LoadSettings();
    }

    public void SetMusicVolume(float volume)
    {
        settingsData.musicVolume = volume;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20); // Устанавливаем громкость
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetGraphicsQuality(int qualityIndex)
    {
        settingsData.graphicsQuality = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("GraphicsQuality", qualityIndex);
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
            settingsData.graphicsQuality = quality;
            QualitySettings.SetQualityLevel(quality);
        }
    }
}