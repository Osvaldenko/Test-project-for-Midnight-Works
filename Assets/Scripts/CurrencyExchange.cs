using UnityEngine;

public class CurrencyExchange : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int coinsCount;
    [SerializeField] private int moneyCount;

    public void BuyCoins()
    {
        if (TakeMoney())
            playerData.AddCoins(coinsCount);
        else Debug.Log("Transaction declined");
    }

    private bool TakeMoney()
    {
        bool isTransactionConfirmed = true;
        //some back code to take "moneyCount" $ from a person
        return isTransactionConfirmed;
    }
}