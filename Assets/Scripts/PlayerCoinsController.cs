using TMPro;
using UnityEngine;

public class PlayerCoinsController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        coinsText.text = playerData.PlayerCoins.ToString();
    }
}