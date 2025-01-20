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
        OnCoinsUpdated?.Invoke();
    }
}