using TMPro;
using UnityEngine;

public class PlayerCoinsController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void OnEnable()
    {
        playerData.OnCoinsUpdated += UpdateCoinsUI;
    }
    private void OnDisable()
    {
        playerData.OnCoinsUpdated -= UpdateCoinsUI;
    }
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            playerData.SetCoins(PlayerPrefs.GetInt("Coins"));
        }
        UpdateCoinsUI();
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = playerData.PlayerCoins.ToString();
    }
}