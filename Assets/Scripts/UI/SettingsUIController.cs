using UnityEngine;
using UnityEngine.UI;

public class SettingsUIController : MonoBehaviour
{
    public SettingsController settingsController;
    public Slider volumeSlider;
    public Dropdown graphicsDropdown;

    private void Start()
    {
        volumeSlider.value = settingsController.settingsData.musicVolume;
        graphicsDropdown.value = settingsController.settingsData.graphicsQuality;

        volumeSlider.onValueChanged.AddListener(settingsController.SetMusicVolume);
        graphicsDropdown.onValueChanged.AddListener(settingsController.SetGraphicsQuality);
    }
}