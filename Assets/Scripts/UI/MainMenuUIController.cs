using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject mainManuPanel;
    [SerializeField] private GameObject currencyShopPanel;
    [SerializeField] private GameObject settingsPanel;
    [Header("Garage UI")]
    [SerializeField] private GameObject garagePanel;
    [SerializeField] private GameObject colorChangePanel;
    [SerializeField] private GameObject upgradesPanel;

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
    }
    private void OnDisable()
    {
        PhotonLauncher.OnWaitingForPlayers -= OpenWaitingPanel;
    }
    private void Awake()
    {
        LoadCarData();
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseCurrencyShopPanel()
    {
        currencyShopPanel.SetActive(false);
    }
    public void OpenCurrencyShopPanel()
    {
        currencyShopPanel.SetActive(true);
    }
    public void CloseGaragePanel()
    {
        garagePanel.SetActive(false);
        mainManuPanel.SetActive(true);
    }
    public void OpenGaragePanel()
    {
        mainManuPanel.SetActive(false);
        garagePanel.SetActive(true);
    }
    public void OpenColorChangePanel()
    {
        garagePanel.SetActive(false);
        colorChangePanel.SetActive(true);
    }
    public void CloseColorChangePanel()
    {
        colorChangePanel.SetActive(false);
        garagePanel.SetActive(true);
    }
    public void OpenUpgradePanel()
    {
        garagePanel.SetActive(false);
        upgradesPanel.SetActive(true);
    }
    public void CloseUpgradePanel()
    {
        upgradesPanel.SetActive(false);
        garagePanel.SetActive(true);
    }
    public void OpenLevelPanel()
    {
        mainManuPanel.SetActive(false);
        levelPanel.SetActive(true);
    }
    public void CloseLevelPanel()
    {
        levelPanel.SetActive(false);
        mainManuPanel.SetActive(true);
    }

    private void OpenWaitingPanel()
    {
        levelPanel.SetActive(false);
        waitingPanel.SetActive(true);
    }
    private void LoadCarData()
    {
        if (PlayerPrefs.HasKey("EngineUpgrade"))
        {
            if (PlayerPrefs.GetInt("EngineUpgrade") == 1)
            {
                engineRemoveButton.SetActive(true);
                engineUpgradeButton.SetActive(false);
            }
            else
            {
                engineRemoveButton.SetActive(false);
                engineUpgradeButton.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey("BackWingsUpgrade"))
        {
            if (PlayerPrefs.GetInt("BackWingsUpgrade") == 1)
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
        if (PlayerPrefs.HasKey("SidesUpgrade"))
        {
            if (PlayerPrefs.GetInt("SidesUpgrade") == 1)
            {
                sidesRemoveButton.SetActive(true);
                sidesUpgradeButton.SetActive(false);
            }
            else
            {
                sidesRemoveButton.SetActive(false);
                sidesUpgradeButton.SetActive(true);
            }
        }
    }
}