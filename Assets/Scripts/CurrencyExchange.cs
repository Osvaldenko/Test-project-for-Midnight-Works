using UnityEngine;
using UnityEngine.UI;

public class CurrencyExchange : MonoBehaviour
{
    [SerializeField] private Button buyBttn;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private int coinsCount;
    [SerializeField] private int moneyCount;

    private void Start()
    {
        buyBttn.onClick.AddListener(() => BuyCoins());
    }
    private void BuyCoins()
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