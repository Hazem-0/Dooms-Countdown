using System;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public float time = 240f;
    public TextMeshProUGUI timerText;
    public GameObject gameOverPanel;

    private bool timerOn = true;

    void Start()
    {
        gameOverPanel.SetActive(false); // Hide Game Over panel initially
        UpdateTimerDisplay(time);
    }

    void Update()
    {
        if (timerOn)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                UpdateTimerDisplay(time);
            }
            else
            {
                time = 0;
                timerOn = false;
                gameOverPanel.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    void UpdateTimerDisplay(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

   
}
