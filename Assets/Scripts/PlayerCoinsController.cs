using TMPro;
using UnityEngine;

public class PlayerCoinsController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void OnEnable()
    {
        playerData.OnCoinsUpdated += UpdateCoinsUI;
        Cheats.OnAllDataDeleted += DataUpdate;
    }
    private void OnDisable()
    {
        playerData.OnCoinsUpdated -= UpdateCoinsUI;
        Cheats.OnAllDataDeleted -= DataUpdate;
    }
    
    private void Start()
    {
        LoadPlaterData();
        UpdateCoinsUI();
    }

    private void DataUpdate()
    {
        LoadPlaterData();
        UpdateCoinsUI();
    }
    private void LoadPlaterData()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            playerData.SetCoins(PlayerPrefs.GetInt("Coins"));
        }
        else
        {
            playerData.SetCoins(0);
        }
    }
    private void UpdateCoinsUI()
    {
        coinsText.text = playerData.PlayerCoins.ToString();
    }
}