using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageController : MonoBehaviour
{
    public static event Action<CarData> OnCarDataLoaded;

    [SerializeField] private CarData playerCarData;
    [SerializeField] private Transform carSpawnTransform;
    [SerializeField] private List<GameObject> carPrefs;
    [SerializeField] private List<CarInfo> carInfos;
    [Header("Garage UI")]
    [SerializeField] private Button selectNextCarBttn;

    [SerializeField] private Button setFirstColorBttn;
    [SerializeField] private Button setSecondColorBttn;
    [SerializeField] private Button setThirdColorBttn;

    [SerializeField] private Button upgradeEngineBttn;
    [SerializeField] private Button removeEngineBttn;
    [SerializeField] private Button upgradeSidesBttn;
    [SerializeField] private Button removeSidesBttn;
    [SerializeField] private Button upgradeBackWingsBttn;
    [SerializeField] private Button removeBackWingsBttn;

    private CarComponentsController carComponentsController;

    private void OnEnable()
    {
        Cheats.OnTaxiSold += SelectNextCar;
        Cheats.OnHammerSold += SelectNextCar;
        Cheats.OnPickUpSold += SelectNextCar;
        Cheats.OnAllDataDeleted += SelectNextCar;
    }
    private void OnDisable()
    {
        Cheats.OnTaxiSold -= SelectNextCar;
        Cheats.OnHammerSold -= SelectNextCar;
        Cheats.OnPickUpSold -= SelectNextCar;
        Cheats.OnAllDataDeleted -= SelectNextCar;
    }
    private void Awake()
    {
        LoadCarData();
    }
    private void Start()
    {
        selectNextCarBttn.onClick.AddListener(() => SelectNextCar());

        setFirstColorBttn.onClick.AddListener(() => SetFirstColor());
        setSecondColorBttn.onClick.AddListener(() => SetSecondColor());
        setThirdColorBttn.onClick.AddListener(() => SetThirdColor());

        upgradeEngineBttn.onClick.AddListener(() => UpgradeEndine());
        removeEngineBttn.onClick.AddListener(() => RemoveEndineUpgrade());
        upgradeSidesBttn.onClick.AddListener(() => UpgradeSides());
        removeSidesBttn.onClick.AddListener(() => RemoveSidesUpgrade());
        upgradeBackWingsBttn.onClick.AddListener(() => UpgradeBackWings());
        removeBackWingsBttn.onClick.AddListener(() => RemoveBackWingsUpgrade());
    }
    private void SetFirstColor()
    {
        SetCarColor(0);
    }
    private void SetSecondColor()
    {
        SetCarColor(1);
    }
    private void SetThirdColor()
    {
        SetCarColor(2);
    }
    ///
    private void UpgradeEndine()
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        playerCarData.SetEngineUpgrade(true);
        carComponentsController.Engine(true);
        PlayerPrefs.SetInt(carKey + "_Engine", playerCarData.IsEngineUpgraded ? 1 : 0);
    }
    private void UpgradeBackWings()
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        playerCarData.SetBackWingsUpgrade(true);
        carComponentsController.BackWings(true);
        PlayerPrefs.SetInt(carKey + "_BackWings", playerCarData.IsBackWingsUpgraded ? 1 : 0);
    }
    private void UpgradeSides()
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        playerCarData.SetSidesUpgrade(true);
        carComponentsController.Sides(true);
        PlayerPrefs.SetInt(carKey + "_Sides", playerCarData.IsSidesUpgraded ? 1 : 0);
    }
    private void RemoveEndineUpgrade()
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        playerCarData.SetEngineUpgrade(false);
        carComponentsController.Engine(false);
        PlayerPrefs.SetInt(carKey + "_Engine", playerCarData.IsEngineUpgraded ? 1 : 0);
    }
    private void RemoveBackWingsUpgrade()
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        playerCarData.SetBackWingsUpgrade(false);
        carComponentsController.BackWings(false);
        PlayerPrefs.SetInt(carKey + "_BackWings", playerCarData.IsBackWingsUpgraded ? 1 : 0);
    }
    private void RemoveSidesUpgrade()
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        playerCarData.SetSidesUpgrade(false);
        carComponentsController.Sides(false);
        PlayerPrefs.SetInt(carKey + "_Sides", playerCarData.IsSidesUpgraded ? 1 : 0);
    }
    ///
    private void SelectNextCar()
    {
        int selectedCarID = 0;
        switch (PlayerPrefs.GetString("SelectedCarName"))
        {
            case "Regular":
                selectedCarID = 0;
                break;
            case "PickUpTruck":
                selectedCarID = 1;
                break;
            case "Hammer":
                selectedCarID = 2;
                break;
            case "Taxi":
                selectedCarID = 3;
                break;
        }
        if (selectedCarID == 3) selectedCarID = 0;
        else selectedCarID++;
        int tick = 0;
        do
        {
            if (PlayerPrefs.GetInt("Car_" + carInfos[selectedCarID].carPriceData.CarName) == (int)CarStatus.Sold)
            {
                if (carInfos[selectedCarID].carPriceData.CarName != PlayerPrefs.GetString("SelectedCarName"))
                {
                    PlayerPrefs.SetString("SelectedCarName", carInfos[selectedCarID].carPriceData.CarName);
                    RemoveCarObject();
                    LoadCarData();
                    Debug.Log("break!");
                    break;
                }
            }
            else
            {
                selectedCarID++;
                if (selectedCarID == 4) selectedCarID = 0;
            }
            tick++;
            Debug.Log("tick!_" + tick);
        } while (tick < carInfos.Count);
        Debug.Log("no car to choose next");
    }

    private void SetCarColor(int colorID)
    {
        string carKey = "Tuning_" + playerCarData.CarName;
        PlayerPrefs.SetInt(carKey + "_MaterialID", colorID);
        foreach (CarInfo carInfo in carInfos)
        {
            if (carInfo.carPriceData.CarName == PlayerPrefs.GetString("SelectedCarName"))
            {
                playerCarData.SetCarMaterial(carInfo.colorMaterials[colorID]);
                carComponentsController.SetCarMaterial(carInfo.colorMaterials[colorID]);
            }
        }
    }
    private void LoadCarData()
    {
        if (PlayerPrefs.HasKey("SelectedCarName"))
        {
            foreach (CarInfo carInfo in carInfos)
            {
                if (carInfo.carPriceData.CarName == PlayerPrefs.GetString("SelectedCarName"))
                {
                    playerCarData.SetCarName(carInfo.carPriceData.CarName);
                    string carKey = "Tuning_" + playerCarData.CarName;
                    playerCarData.SetCarMaterial(carInfo.colorMaterials[PlayerPrefs.GetInt(carKey + "_MaterialID")]);
                    playerCarData.SetEngineUpgrade(PlayerPrefs.GetInt(carKey + "_Engine") == 1);
                    playerCarData.SetSidesUpgrade(PlayerPrefs.GetInt(carKey + "_Sides") == 1);
                    playerCarData.SetBackWingsUpgrade(PlayerPrefs.GetInt(carKey + "_BackWings") == 1);

                    int prefID = 0;
                    switch (PlayerPrefs.GetString("SelectedCarName"))
                    {
                        case "Regular":
                            prefID = 0;
                            break;
                        case "PickUpTruck":
                            prefID = 1;
                            break;
                        case "Hammer":
                            prefID = 2;
                            break;
                        case "Taxi":
                            prefID = 3;
                            break;
                    }
                    GameObject car = Instantiate(carPrefs[prefID], carSpawnTransform.position, Quaternion.Euler(0, 180, 0), carSpawnTransform);
                    carComponentsController = car.GetComponent<CarComponentsController>();
                    car.GetComponent<Rigidbody>().useGravity = false;
                }
            }
        }
        else
        {
            PlayerPrefs.SetInt("Car_" + carInfos[0].carPriceData.CarName, (int)CarStatus.Sold);
            PlayerPrefs.SetString("SelectedCarName", carInfos[0].carPriceData.CarName);
            playerCarData.SetCarName(carInfos[0].carPriceData.CarName);
            playerCarData.SetCarMaterial(carInfos[0].colorMaterials[0]);
            playerCarData.SetEngineUpgrade(false);
            playerCarData.SetSidesUpgrade(false);
            playerCarData.SetBackWingsUpgrade(false);

            string carKey = "Tuning_" + playerCarData.CarName;
            PlayerPrefs.SetInt(carKey + "_Engine", playerCarData.IsEngineUpgraded ? 1 : 0);
            PlayerPrefs.SetInt(carKey + "_Sides", playerCarData.IsSidesUpgraded ? 1 : 0);
            PlayerPrefs.SetInt(carKey + "_BackWings", playerCarData.IsBackWingsUpgraded ? 1 : 0);
            PlayerPrefs.SetInt(carKey + "_MaterialID", 0);
            GameObject car = Instantiate(carPrefs[0], carSpawnTransform.position, Quaternion.Euler(0, 180, 0), carSpawnTransform);
            carComponentsController = car.GetComponent<CarComponentsController>();
            car.GetComponent<Rigidbody>().useGravity = false;
        }
        OnCarDataLoaded?.Invoke(playerCarData);
    }
    private void RemoveCarObject()
    {
        foreach (Transform car in carSpawnTransform)
        {
            Destroy(car.gameObject);
        }
    }

    [Serializable]
    private struct CarInfo
    {
        public CarPriceData carPriceData;
        public List<Material> colorMaterials;
    }
}