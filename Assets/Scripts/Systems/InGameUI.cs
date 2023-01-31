using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    // This is here so the level knows about its target score when started directly from the editor
    [SerializeField] private int levelID;

    [SerializeField] private GameObject activeLayout;
    [SerializeField] private GameObject pauseLayout;
    [SerializeField] private GameObject winLayout;
    [SerializeField] private GameObject loseLayout;

    private bool gameIsPaused = false;
    private bool gameIsOver = false;

    private void Awake()
    {
        GameManager.gameUIManager = this;
        GameManager.loadedLevelID = levelID;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }

    // If the game is paused when called, unpauses the game, and vice versa.
    public void TogglePause()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0 : 1;
        pauseLayout.SetActive(gameIsPaused);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        GameManager.ResetGameVars(levelID);
        LevelManager.PlayLevel(levelID);
    }

    public void Win()
    {
        // Called when the final marble is scored for the level, and the current score is sufficient to consider the level "won".
        winLayout.SetActive(true);
    }

    public void Lose()
    {
        // Called when the final marble is scored for the level, and the current score is NOT ENOUGH to win.
        loseLayout.SetActive(true);
    }

    public void BackToMain()
    {
        Time.timeScale = 1;
        LevelManager.LoadMainScene();
    }
}
