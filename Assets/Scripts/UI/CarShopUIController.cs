using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarShopUIController : MonoBehaviour
{
    [SerializeField] private List<CarPriceData> availableCars;
    [SerializeField] private Transform carListParent;
    [SerializeField] private GameObject carButtonPrefab;
    [SerializeField] private PlayerData playerData;

    //private int playerCoins;
    private int playerDriftPoints;

    private void Awake()
    {
        LoadPlayerData();
    }
    private void Start()
    {
        GenerateCarShopUI();
    }
    private void LoadPlayerData()
    {
        //playerCoins = PlayerPrefs.GetInt("Coins", 0);
        playerDriftPoints = PlayerPrefs.GetInt("DriftPoints", 0);
    }
    private void SavePlayerData()
    {
        //PlayerPrefs.SetInt("Coins", playerCoins);
        PlayerPrefs.SetInt("DriftPoints", playerDriftPoints);
    }
    private void GenerateCarShopUI()
    {
        foreach (CarPriceData car in availableCars)
        {
            GameObject carButton = Instantiate(carButtonPrefab, carListParent);
            TMP_Text buttonText = carButton.GetComponentInChildren<TMP_Text>();

            if (PlayerPrefs.GetInt("Car_" + car.CarName, (int)CarStatus.OnSale) == (int)CarStatus.Sold)
            {
                buttonText.text = car.CarName + " (Sold)";
                carButton.GetComponent<Button>().interactable = false;
            }
            else if (PlayerPrefs.GetInt("Car_" + car.CarName, (int)CarStatus.OnSale) == (int)CarStatus.Unlocked)
            {
                buttonText.text = car.CarName + " - " + car.Price + " Coins";
                carButton.GetComponent<Button>().onClick.AddListener(() => BuyCar(car));
            }
            else if (car.RequiredDriftPoints > 0 && playerDriftPoints < car.RequiredDriftPoints)
            {
                buttonText.text = playerDriftPoints + " of " + car.RequiredDriftPoints + " drift points (Need more drift to unclock)";
                carButton.GetComponent<Button>().interactable = false;
                //carButton.GetComponent<Button>().onClick.AddListener(() => UnlockCar(car));
            }
            else if (car.RequiredDriftPoints > 0 && playerDriftPoints >= car.RequiredDriftPoints)
            {
                buttonText.text = playerDriftPoints + " of " + car.RequiredDriftPoints + " drift points (Click to unlock)";
                carButton.GetComponent<Button>().onClick.AddListener(() => UnlockCar(car));
            }
            else
            {
                buttonText.text = car.CarName + " - " + car.Price + " Coins";
                carButton.GetComponent<Button>().onClick.AddListener(() => BuyCar(car));
            }
        }
    }
    private void RefreshShop()
    {
        foreach (Transform child in carListParent)
        {
            Destroy(child.gameObject);
        }
        GenerateCarShopUI();
    }

    public void BuyCar(CarPriceData car)
    {
        Debug.Log("trying to buy car_" + car.CarName);
        if (playerData.PlayerCoins >= car.Price)
        {
            Debug.Log(car.CarName + "_bought");
            playerData.RemoveCoins(car.Price);
            PlayerPrefs.SetInt("Car_" + car.CarName, (int)CarStatus.Sold);
            //SavePlayerData();
            RefreshShop();
        }
    }
    public void UnlockCar(CarPriceData car)
    {
        Debug.Log(car.CarName + "_unlocked");
        PlayerPrefs.SetInt("Car_" + car.CarName, (int)CarStatus.Unlocked);
        playerDriftPoints -= car.RequiredDriftPoints;
        SavePlayerData();
        RefreshShop();
    }
}
public enum CarStatus
{
    OnSale = 0,
    Unlocked = 1,
    Sold = 2
}