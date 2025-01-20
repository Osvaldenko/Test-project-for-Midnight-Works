using UnityEngine;

public class CurrencyExchange : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int coinsCount;

    public void BuyCoins()
    {
        playerData.AddCoins(coinsCount);
    }
}