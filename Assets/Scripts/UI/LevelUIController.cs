using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private PlayerScoreGenerator scoreGenerator;
    [Header("GameUI")]
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI driftingScoreText;
    [SerializeField] private GameObject driftingScoreGameObject;
    [Header("EndGameUI")]
    [SerializeField] private TextMeshProUGUI premiumText;
    [SerializeField] private TextMeshProUGUI playerCoinsText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject aDPanel;
    [SerializeField] private TextMeshProUGUI aDText;
    [SerializeField] private Button aDButton;
    [SerializeField] private Button exitButton;

    private int playerCoins;

    private void OnEnable()
    {
        TimerUIController.onGameOver += GameOver;
        IronSourcePCController.OnAdStarted += OnAdStarted;
        IronSourcePCController.OnAdCompletedEvent += OnAdCompletedEvent;
    }
    private void OnDisable()
    {
        TimerUIController.onGameOver -= GameOver;
        IronSourcePCController.OnAdStarted -= OnAdStarted;
        IronSourcePCController.OnAdCompletedEvent -= OnAdCompletedEvent;
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Premium") == 1)
        {
            premiumText.gameObject.SetActive(true);
        }
        exitButton.onClick.AddListener(() => ExitToMenu());
    }
    private void OnAdCompletedEvent()
    {
        playerCoins *= 2;
        playerCoinsText.text = "Coins for drift: " + playerCoins.ToString();
        aDButton.interactable = false;
        aDPanel.SetActive(false);
    }
    private void OnAdStarted(float ADtime)
    {
        aDText.text = "AD will end in " + ADtime.ToString() + " seconds..";
        aDPanel.SetActive(true);
    }
    private void GameOver()
    {
        //PlayerPrefs.SetInt("DriftPoints", scoreGenerator.PlayerScore);
        playerCoins = scoreGenerator.PlayerScore / 3;
        if (PlayerPrefs.GetInt("Premium") == 1)
        {
            playerCoins *= 2;
        }
        StartCoroutine(EndGame());
    }
    private void ExitToMenu()
    {
        scoreGenerator.AddCoinsToPlayer(playerCoins);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SetPlayerScoreText(int value)
    {
        playerScoreText.text = value.ToString();
    }
    public bool CheckIfActiveDriftingScoreObject()
    {
        return driftingScoreGameObject.activeSelf;
    }
    public void SetActiveDriftingScoreObject(bool value)
    {
        driftingScoreGameObject.SetActive(value);
    }
    public void UpdateDriftingScoreText(int value)
    {
        driftingScoreText.text = value.ToString();
    }
    public void UpdatePlayerScoreText(int value)
    {
        playerScoreText.text = value.ToString();
    }
    IEnumerator EndGame()
    {
        playerCoinsText.text = "Coins for drift: " + playerCoins.ToString();
        yield return new WaitForSeconds(1.5f);
        endGamePanel.SetActive(true);
    }
}