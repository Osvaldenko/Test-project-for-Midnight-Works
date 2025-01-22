using UnityEngine;

[CreateAssetMenu(fileName = "SettingsData", menuName = "ScriptableObjects/SettingsData")]
public class SettingsData : ScriptableObject
{
    [Range(0f, 1f)]
    [SerializeField] public float musicVolume = 1f;
    [SerializeField] public GraphicsQuality graphicsQuality;
}

public enum GraphicsQuality
{
    LowQuality = 0,
    MediumQuality = 1,
    HighQuality = 2
};