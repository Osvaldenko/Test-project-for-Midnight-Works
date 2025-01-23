using System.Collections.Generic;
using UnityEngine;

public class GarageController : MonoBehaviour
{
    [SerializeField] private CarData playerCarData;
    [SerializeField] private List<Material> colorMaterials;
    [SerializeField] private CarComponentsController carComponentsController;

    private void Awake()
    {
        LoadCarData();
    }
    public void SetWhiteColor()
    {
        SetCarColor(0);
    }
    public void SetBlueColor()
    {
        SetCarColor(1);
    }
    public void SetBlackColor()
    {
        SetCarColor(2);
    }
    public void UpgradeEndine()
    {
        PlayerPrefs.SetInt("EngineUpgrade", 1);
        playerCarData.SetEngineUpgrade(true);
        carComponentsController.Endine(true);
    }
    public void UpgradeBackWings()
    {
        PlayerPrefs.SetInt("BackWingsUpgrade", 1);
        playerCarData.SetBackWingsUpgrade(true);
        carComponentsController.BackWings(true);
    }
    public void UpgradeSides()
    {
        PlayerPrefs.SetInt("SidesUpgrade", 1);
        playerCarData.SetSidesUpgrade(true);
        carComponentsController.Sides(true);
    }
    public void RemoveEndineUpgrade()
    {
        PlayerPrefs.SetInt("EngineUpgrade", 0);
        playerCarData.SetEngineUpgrade(false);
        carComponentsController.Endine(false);
    }
    public void RemoveBackWingsUpgrade()
    {
        PlayerPrefs.SetInt("BackWingsUpgrade", 0);
        playerCarData.SetBackWingsUpgrade(false);
        carComponentsController.BackWings(false);
    }
    public void RemoveSidesUpgrade()
    {
        PlayerPrefs.SetInt("SidesUpgrade", 0);
        playerCarData.SetSidesUpgrade(false);
        carComponentsController.Sides(false);
    }

    private void SetCarColor(int colorID)
    {
        PlayerPrefs.SetInt("CarMaterial", colorID);
        playerCarData.SetCarMaterial(colorMaterials[colorID]);
        carComponentsController.SetCarMaterial(colorMaterials[colorID]);
    }
    private void LoadCarData()
    {
        if (PlayerPrefs.HasKey("CarMaterial"))
        {
            switch (PlayerPrefs.GetInt("CarMaterial"))
            {
                case 0:
                    playerCarData.SetCarMaterial(colorMaterials[0]);
                    break;
                case 1:
                    playerCarData.SetCarMaterial(colorMaterials[1]);
                    break;
                case 2:
                    playerCarData.SetCarMaterial(colorMaterials[2]);
                    break;
            }
        }
        if(PlayerPrefs.HasKey("EngineUpgrade"))
        {
            if (PlayerPrefs.GetInt("EngineUpgrade") == 1)
            {
                playerCarData.SetEngineUpgrade(true);
            }
        }
        if (PlayerPrefs.HasKey("BackWingsUpgrade"))
        {
            if (PlayerPrefs.GetInt("BackWingsUpgrade") == 1)
            {
                playerCarData.SetBackWingsUpgrade(true);
            }
        }
        if (PlayerPrefs.HasKey("SidesUpgrade"))
        {
            if (PlayerPrefs.GetInt("SidesUpgrade") == 1)
            {
                playerCarData.SetSidesUpgrade(true);
            }
        }
    }
}