using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    [SerializeField] private SettingsController settingsController;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown graphicsDropdown;

    private void OnEnable()
    {
        settingsController.OnSettingsLoaded += OnSettingsLoaded;
    }
    private void OnDisable()
    {
        settingsController.OnSettingsLoaded -= OnSettingsLoaded;
    }

    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(settingsController.SetMusicVolume);
        graphicsDropdown.onValueChanged.AddListener(settingsController.SetGraphicsQuality);
    }

    private void OnSettingsLoaded()
    {
        volumeSlider.value = settingsController.settingsData.musicVolume;
        graphicsDropdown.value = (int)settingsController.settingsData.graphicsQuality;
    }
}