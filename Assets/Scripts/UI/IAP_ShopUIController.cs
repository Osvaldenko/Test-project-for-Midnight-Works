using UnityEngine;
using UnityEngine.UI;

public class IAP_ShopUIController : MonoBehaviour
{
    [SerializeField] private Button premiumBuyBttn;
    [SerializeField] private Button removeADBuyBttn;

    private void OnEnable()
    {
        Cheats.OnAllDataDeleted += LoadData;
        Cheats.OnPremiumRemoved += LoadData;
    }
    private void OnDisable()
    {
        Cheats.OnAllDataDeleted -= LoadData;
        Cheats.OnPremiumRemoved -= LoadData;
    }
    private void Awake()
    {
        LoadData();
    }
    private void Start()
    {
        premiumBuyBttn.onClick.AddListener(() => BuyPremium());
        removeADBuyBttn.onClick.AddListener(() => BuyRemoveAD());
    }

    private void LoadData()
    {
        if (PlayerPrefs.GetInt("Premium") == 1)
        {
            premiumBuyBttn.interactable = false;
        }
        else
        {
            premiumBuyBttn.interactable = true;
        }
        if (PlayerPrefs.GetInt("RemoveAD") == 1)
        {
            removeADBuyBttn.interactable = false;
        }
        else
        {
            removeADBuyBttn.interactable = true;
        }
    }
    private void BuyPremium()
    {
        if (TakeMoney(10))
        {
            PlayerPrefs.SetInt("Premium", 1);
            premiumBuyBttn.interactable = false;
        }
    }
    private void BuyRemoveAD()
    {
        if (TakeMoney(10))
        {
            PlayerPrefs.SetInt("RemoveAD", 1);
            removeADBuyBttn.interactable = false;
        }
    }
    private bool TakeMoney(float money)
    {
        bool isTransactionConfirmed = true;
        //some back code to take "moneyCount" $ from a person
        return isTransactionConfirmed;
    }
}