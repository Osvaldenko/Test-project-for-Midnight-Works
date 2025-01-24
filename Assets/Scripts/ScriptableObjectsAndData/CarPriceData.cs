using UnityEngine;

[CreateAssetMenu(fileName = "CarPriceData", menuName = "ScriptableObjects/CarPrice")]
public class CarPriceData : ScriptableObject
{
    [SerializeField] private string carName;
    [SerializeField] private int price;
    [SerializeField] private int requiredDriftPoints;
    [SerializeField] private bool isUnlockedByDefault;

    public string CarName { get { return carName; } }
    public int Price { get { return price;} }
    public int RequiredDriftPoints { get { return requiredDriftPoints;} }
    public bool IsUnlockedByDefault { get { return isUnlockedByDefault; } }
}