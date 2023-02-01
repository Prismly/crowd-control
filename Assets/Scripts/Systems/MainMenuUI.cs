using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [SerializeField] private OvenExterior ovenMatSelector;

    [SerializeField] private MenuRotation menuRot;
    [SerializeField] private LevelMenuScroller lvlMenuScrl;
    [SerializeField] private TextMeshProUGUI levelNameText;

    private int selectedLevelNum = 1;

    private void Start()
    {
        SelectLevel(1);
        if (!loadIntoLevelSelect)
        {
            // First load-up; boot up on main menu
            SwitchToMain();
        }
        else
        {
            // Subsequent load-up; boot up in level select
            SwitchToLevelSelect();
        }
    }

    // moveInc is -1 if selecting the previous level, and 1 otherwise
    public void SelectAdjacentLevel(int moveInc)
    {
        int newSelected = selectedLevelNum + moveInc;
        if (newSelected < 1 || newSelected > LevelManager.GetLevelCount())
        {
            // Tried to select out of bounds!
            return;
        }

        // Selecting in-bounds
        SelectLevel(newSelected);

        // Spin
        menuRot.SetRotIsNegative(new Vector3Int(1, moveInc, 1));
        menuRot.SwivelAndSlow(new Vector3(0, 40, 0));
        //lvlMenuScrl.EnableScroll(moveInc);
    }

    private void SelectLevel(int levelNum)
    {
        selectedLevelNum = levelNum;
        levelNameText.text = LevelManager.GetLevelName(selectedLevelNum);
        ovenMatSelector.UseMatForLvl(levelNum);
    }

    // Called by UI to activate the Level Select layout.
    public void SwitchToLevelSelect()
    {
        mainMenuLayout.SetActive(false);
        levelSelectLayout.SetActive(true);
        menuRot.SetRotPerSec(Vector3.zero);
    }

    // Called by UI to activate the Main Menu layout.
    public void SwitchToMain()
    {
        mainMenuLayout.SetActive(true);
        levelSelectLayout.SetActive(false);
        menuRot.SetRotPerSec(new Vector3(0, 20, 0));
    }

    // Called by UI to start a level.
    public void LoadLevel(int levelNum)
    {
        LevelManager.PlayLevel(levelNum);
    }

    public void LoadSelectedLevel()
    {
        LevelManager.PlayLevel(selectedLevelNum);
    }

    // Called by UI to close the game.
    public void CloseApplication()
    {
        Application.Quit();
    }
}
