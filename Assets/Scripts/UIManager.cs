using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject levelSelectUI;

    public void MoveToLevelSelect()
    {
        // Hide all Main Menu buttons and text
        mainMenuUI.SetActive(false);

        // TODO: transition lerping between different menus, currently instant

        // Reveal all Level Select buttons and text
        levelSelectUI.SetActive(true);
    }

    public void MoveToMainMenu()
    {
        // Hide all Main Menu buttons and text
        levelSelectUI.SetActive(false);

        // TODO: transition lerping between different menus, currently instant

        // Reveal all Level Select buttons and text
        mainMenuUI.SetActive(true);
    }

    // Closes the game
    public void QuitGame()
    {
        Application.Quit();
    }

    // Begin a game, using the board corresponding to the given ID
    public void StartGame(int levelID)
    {
        Debug.Log("Nothing here yet!");
    }
}