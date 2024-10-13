using UnityEngine;

public class CarComponentsController : MonoBehaviour
{
    [SerializeField] private CarData carData;
    [Header("Car parts")]
    [SerializeField] private GameObject backWingsObject;
    [SerializeField] private GameObject enginePartObject;
    [SerializeField] private GameObject sidesObject;
    [SerializeField] private MeshRenderer body;

    private void Start()
    {
        SetCarSettings();
    }

    public void UpgradeBackWings()
    {
        backWingsObject.SetActive(true);
    }
    public void UpgradeSides()
    {
        sidesObject.SetActive(true);
    }
    public void UpgradeEndine()
    {
        enginePartObject.SetActive(true);
    }
    public void SetCarMaterial(Material material)
    {
        body.material = material;
    }
    private void SetCarSettings()
    {
        body.material = carData.PlayerCarMaterial;
        if (carData.IsEngineUpgraded)
        {
            enginePartObject.SetActive(true);
        }
        if (carData.IsBackWingsUpgraded)
        {
            backWingsObject.SetActive(true);
        }
        if (carData.iIsSidesUpgraded)
        {
            sidesObject.SetActive(true);
        }
    }
}