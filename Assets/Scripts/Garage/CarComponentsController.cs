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

    public void BackWings(bool value)
    {
        backWingsObject.SetActive(value);
    }
    public void Sides(bool value)
    {
        sidesObject.SetActive(value);
    }
    public void Engine(bool value)
    {
        enginePartObject.SetActive(value);
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
        if (carData.IsSidesUpgraded)
        {
            sidesObject.SetActive(true);
        }
    }
}