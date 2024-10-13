using System.Collections;
using UnityEngine;

public class PlayerScoreGenerator : MonoBehaviour
{
    [SerializeField] private LevelUIController levelUIController;
    [SerializeField] private PlayerData playerData;

    public int PlayerScore { get { return playerScore; } }

    private int playerScore = 0;
    private int driftingScore = 0;

    private void OnEnable()
    {
        CarController.OnCarDrifting += OnCarDrifting;
        CarController.OnCarStopedDrifting += OnCarStopedDrifting;
    }
    private void OnDisable()
    {
        CarController.OnCarDrifting -= OnCarDrifting;
        CarController.OnCarStopedDrifting -= OnCarStopedDrifting;
    }
    private void Start()
    {
        levelUIController.SetPlayerScoreText(playerScore);
    }

    public void AddCoinsToPlayer(int value)
    {
        playerData.AddCoins(value);
    }
    private void OnCarDrifting()
    {
        if (levelUIController.CheckIfActiveDriftingScoreObject() == false) levelUIController.SetActiveDriftingScoreObject(true);
        driftingScore++;
        levelUIController.UpdateDriftingScoreText(driftingScore);
    }
    private void OnCarStopedDrifting()
    {
        StartCoroutine(ScoreCountDown());
    }

    IEnumerator ScoreCountDown()
    {
        yield return new WaitForSeconds(1);
        levelUIController.SetActiveDriftingScoreObject(false);
        playerScore += driftingScore;
        driftingScore = 0;
        levelUIController.UpdatePlayerScoreText(playerScore);
    }
}