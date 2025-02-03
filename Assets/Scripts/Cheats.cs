using System;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    public static event Action OnAllDataDeleted;
    public static event Action OnPremiumRemoved;
    public static event Action On10kDriftPointsAdded;
    public static event Action OnTaxiSold;
    public static event Action OnHammerSold;
    public static event Action OnPickUpSold;

    [SerializeField] private Button deleteAllDataBttn;
    [SerializeField] private Button sellHammerBttn;
    [SerializeField] private Button sellPickUpBttn;
    [SerializeField] private Button sellTaxiBttn;
    [SerializeField] private Button removePremiumBttn;
    [SerializeField] private Button add10kDriftPointsBttn;

    [SerializeField] private CarPriceData taxiData;
    [SerializeField] private CarPriceData hammerData;
    [SerializeField] private CarPriceData pickupData;

    private void Start()
    {
        deleteAllDataBttn.onClick.AddListener(() => DeleteAllData());
        sellHammerBttn.onClick.AddListener(() => SellHammer());
        sellPickUpBttn.onClick.AddListener(() => SellPickUp());
        sellTaxiBttn.onClick.AddListener(() => SellTaxi());
        removePremiumBttn.onClick.AddListener(() => RemovePremium());
        add10kDriftPointsBttn.onClick.AddListener(() => Add10kDriftPoints());
    }

    private void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        OnAllDataDeleted?.Invoke();
    }
    private void SellHammer()
    {
        string carKey = "Tuning_" + hammerData.CarName;
        PlayerPrefs.SetInt("Car_" + hammerData.CarName, (int)CarStatus.OnSale);
        PlayerPrefs.SetInt(carKey + "_Engine", 0);
        PlayerPrefs.SetInt(carKey + "_BackWings", 0);
        PlayerPrefs.SetInt(carKey + "_Sides", 0);
        OnHammerSold.Invoke();
    }
    private void SellPickUp()
    {
        string carKey = "Tuning_" + pickupData.CarName;
        PlayerPrefs.SetInt("Car_" + pickupData.CarName, (int)CarStatus.OnSale);
        PlayerPrefs.SetInt(carKey + "_Engine", 0);
        PlayerPrefs.SetInt(carKey + "_BackWings", 0);
        PlayerPrefs.SetInt(carKey + "_Sides", 0);
        OnPickUpSold.Invoke();
    }
    private void SellTaxi()
    {
        string carKey = "Tuning_" + taxiData.CarName;
        PlayerPrefs.SetInt("Car_" + taxiData.CarName, (int)CarStatus.OnSale);
        PlayerPrefs.SetInt(carKey + "_Engine", 0);
        PlayerPrefs.SetInt(carKey + "_BackWings", 0);
        PlayerPrefs.SetInt(carKey + "_Sides", 0);
        OnTaxiSold.Invoke();
    }
    private void RemovePremium()
    {
        PlayerPrefs.SetInt("Premium", 0);
        OnPremiumRemoved?.Invoke();
    }
    private void Add10kDriftPoints()
    {
        PlayerPrefs.SetInt("DriftPoints", PlayerPrefs.GetInt("DriftPoints") + 10000);
        On10kDriftPointsAdded?.Invoke();
    }
}