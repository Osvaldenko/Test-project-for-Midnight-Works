using System;
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    public static event Action onGameOver;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startTimeInSeconds = 120f;

    private float timer;
    private bool isGameOver = false;

    void Start()
    {
        timer = startTimeInSeconds;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            if (isGameOver == false)
            {
                isGameOver = true;
                timer = 0;
                onGameOver?.Invoke();
            }
        }

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}