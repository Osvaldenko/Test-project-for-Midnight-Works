using System.Collections.Generic;
using UnityEngine;

public class GarageController : MonoBehaviour
{
    [SerializeField] private CarData playerCarData;
    [SerializeField] private List<Material> colorMaterials;
    [SerializeField] private CarComponentsController carComponentsController;

    public void SetWhiteColor()
    {
        playerCarData.SetCarMaterial(colorMaterials[0]);
        carComponentsController.SetCarMaterial(colorMaterials[0]);
    }
    public void SetBlueColor()
    {
        playerCarData.SetCarMaterial(colorMaterials[1]);
        carComponentsController.SetCarMaterial(colorMaterials[1]);
    }
    public void SetBlackColor()
    {
        playerCarData.SetCarMaterial(colorMaterials[2]);
        carComponentsController.SetCarMaterial(colorMaterials[2]);
    }
    public void UpgradeEndine()
    {
        playerCarData.SetEngineUpgrade(true);
        carComponentsController.UpgradeEndine();
    }
    public void UpgradeBackWings()
    {
        playerCarData.SetBackWingsUpgrade(true);
        carComponentsController.UpgradeBackWings();
    }
    public void UpgradeSides()
    {
        playerCarData.SetSidesUpgrade(true);
        carComponentsController.UpgradeSides();
    }
}