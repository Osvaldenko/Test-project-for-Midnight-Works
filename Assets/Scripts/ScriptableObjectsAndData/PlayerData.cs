using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player")]
public class PlayerData : ScriptableObject
{
    public event Action OnCoinsUpdated;

    [SerializeField] private int playerCoins;

    public int PlayerCoins { get { return playerCoins; } }

    public void AddCoins(int value)
    {
        playerCoins += value;
        PlayerPrefs.SetInt("Coins", playerCoins);
        OnCoinsUpdated?.Invoke();
    }
    public void RemoveCoins(int value)
    {
        playerCoins -= value;
        PlayerPrefs.SetInt("Coins", playerCoins);
        OnCoinsUpdated?.Invoke();
    }
    public void SetCoins(int value)
    {
        playerCoins = value;
    }
}