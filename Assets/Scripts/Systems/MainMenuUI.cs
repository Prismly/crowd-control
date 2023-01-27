using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains functionality for UI elements of the Main Menu and Level Select portions of the game's startup Scene.
public class MainMenuUI : MonoBehaviour
{
    // Used on startup to determine which of the two layouts -- Main Menu or Level Select -- to load into.
    // Should be assigned before loading the scene to achieve the desired effect.
    private static bool loadIntoLevelSelect = false;

    // Handy enum to refer to one layout over another.
    private enum MAIN_MENU_STATE
    {
        MAIN,
        L_SELECT
    }

    // Container objects for activating/deactivating UI layouts.
    [SerializeField] private GameObject mainMenuLayout;
    [SerializeField] private GameObject levelSelectLayout;

    private void Start()
    {
        if (!loadIntoLevelSelect)
        {
            // First load-up; boot up on main menu
            ActivateLayout(MAIN_MENU_STATE.MAIN);
        }
        else
        {
            // Subsequent load-up; boot up in level select
            ActivateLayout(MAIN_MENU_STATE.L_SELECT);
        }
    }

    // Makes the given layout visible, and the other invisible. Ignores which is already visible.
    private void ActivateLayout(MAIN_MENU_STATE targetLayout)
    {
        mainMenuLayout.SetActive(targetLayout == MAIN_MENU_STATE.MAIN);
        levelSelectLayout.SetActive(targetLayout == MAIN_MENU_STATE.L_SELECT);
    }

    // Called by UI to activate the Level Select layout.
    public void SwitchToLevelSelect()
    {
        ActivateLayout(MAIN_MENU_STATE.L_SELECT);
    }

    // Called by UI to activate the Main Menu layout.
    public void SwitchToMain()
    {
        ActivateLayout(MAIN_MENU_STATE.MAIN);
    }

    // Called by UI to start a level.
    public void LoadLevel(int levelNum)
    {
        LevelManager.PlayLevel(levelNum);
    }

    // Called by UI to close the game.
    public void CloseApplication()
    {
        Application.Quit();
    }
}
