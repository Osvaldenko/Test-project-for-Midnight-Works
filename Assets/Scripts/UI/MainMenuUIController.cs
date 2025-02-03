using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [Header("Garage UI")]
    [SerializeField] private GameObject engineUpgradeButton;
    [SerializeField] private GameObject engineRemoveButton;

    [SerializeField] private GameObject wingsUpgradeButton;
    [SerializeField] private GameObject wingsRemoveButton;

    [SerializeField] private GameObject sidesUpgradeButton;
    [SerializeField] private GameObject sidesRemoveButton;
    [Header("LevelSelect UI")]
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject waitingPanel;

    private void OnEnable()
    {
        PhotonLauncher.OnWaitingForPlayers += OpenWaitingPanel;
        GarageController.OnCarDataLoaded += UpdateGarageTuningUI;
    }
    private void OnDisable()
    {
        PhotonLauncher.OnWaitingForPlayers -= OpenWaitingPanel;
        GarageController.OnCarDataLoaded -= UpdateGarageTuningUI;
    }

    private void OpenWaitingPanel()
    {
        levelPanel.SetActive(false);
        waitingPanel.SetActive(true);
    }
    private void UpdateGarageTuningUI(CarData carData)
    {
        string carKey = "Tuning_" + carData.CarName;
        if(PlayerPrefs.GetInt(carKey + "_Engine") == 1)
        {
            engineRemoveButton.SetActive(true);
            engineUpgradeButton.SetActive(false);
        }
        else
        {
            engineRemoveButton.SetActive(false);
            engineUpgradeButton.SetActive(true);
        }
        if(PlayerPrefs.GetInt(carKey + "_Sides") == 1)
        {
            sidesRemoveButton.SetActive(true);
            sidesUpgradeButton.SetActive(false);
        }
        else
        {
            sidesRemoveButton.SetActive(false);
            sidesUpgradeButton.SetActive(true);
        }
        if(PlayerPrefs.GetInt(carKey + "_BackWings") == 1)
        {
            wingsRemoveButton.SetActive(true);
            wingsUpgradeButton.SetActive(false);
        }
        else
        {
            wingsRemoveButton.SetActive(false);
            wingsUpgradeButton.SetActive(true);
        }
    }
}