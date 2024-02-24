using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDMenu : Menu
{

    public MenuClassifier pauseMenuClassifier;

    [SerializeField]
    private TextMeshProUGUI healthCounter;
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private Button pauseButton;
   
    private bool isPaused = false;
    private float timer = 0f;

  


    private void Update()
    {
        if (!isPaused)
        {
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2");
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        MenuManager.Instance.ShowMenu(isPaused ? pauseMenuClassifier : null);
    }

    public void hideMenu()
    {
        //if (isPaused)
        //{
        //    // Resume the game time and update the isPaused state
        //    TogglePause();
        //}
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // Now, we can safely hide the pause menu using the MenuManager
        MenuManager.Instance.HideMenu(pauseMenuClassifier);
    }

    public void SetHealth(int health)
    {
        healthCounter.text = health.ToString();
    }

    // Make sure to link this to your pause button's OnClick event
    public void OnPauseButtonPressed()
    {
        TogglePause();
    }
}
