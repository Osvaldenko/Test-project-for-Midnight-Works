using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "ScriptableObjects/Car")]
public class CarData : ScriptableObject
{
    [SerializeField] private Material playerCarMaterial;
    [SerializeField] private bool isEngineUpgraded;
    [SerializeField] private bool isSidesUpgraded;
    [SerializeField] private bool isBackWingsUpgraded;

    public Material PlayerCarMaterial { get { return playerCarMaterial; } }
    public bool IsEngineUpgraded { get { return isEngineUpgraded; } }
    public bool iIsSidesUpgraded { get { return isSidesUpgraded; } }
    public bool IsBackWingsUpgraded { get { return isBackWingsUpgraded; } }

    public void SetCarMaterial(Material material)
    {
        playerCarMaterial = material;
    }
    public void SetEngineUpgrade(bool value)
    {
        isEngineUpgraded = value;
    }
    public void SetSidesUpgrade(bool value)
    {
        isSidesUpgraded = value;
    }
    public void SetBackWingsUpgrade(bool value)
    {
        isBackWingsUpgraded = value;
    }
}