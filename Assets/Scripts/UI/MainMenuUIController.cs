using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject mainManuPanel;
    [SerializeField] private GameObject currencyShopPanel;
    [Header("Garage UI")]
    [SerializeField] private GameObject garagePanel;
    [SerializeField] private GameObject colorChangePanel;
    [SerializeField] private GameObject upgradesPanel;
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
}