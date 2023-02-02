using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        L_SELECT,
        SETTINGS
    }

    private MAIN_MENU_STATE menuState = MAIN_MENU_STATE.MAIN;

    // Container objects for activating/deactivating UI layouts.
    [SerializeField] private GameObject mainMenuLayout;
    [SerializeField] private GameObject levelSelectLayout;
    [SerializeField] private GameObject settingsLayout;

    [SerializeField] private Slider volumeSlider;

    [SerializeField] private OvenExterior ovenMatSelector;

    [SerializeField] private MenuRotation menuRot;
    [SerializeField] private LevelMenuScroller lvlMenuScrl;
    [SerializeField] private BobbingText levelNameText;

    [SerializeField] private AudioSource levelSelectSwoosh;

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

        levelSelectSwoosh.Play();

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
        levelNameText.SetText(LevelManager.GetLevelName(selectedLevelNum));
        ovenMatSelector.UseMatForLvl(levelNum);
        GameManager.loadedLevelID = levelNum;
    }

    // Called by UI to activate the Level Select layout.
    public void SwitchToLevelSelect()
    {
        menuState = MAIN_MENU_STATE.L_SELECT;
        mainMenuLayout.SetActive(false);
        levelSelectLayout.SetActive(true);
        settingsLayout.SetActive(false);
        menuRot.SetRotPerSec(Vector3.zero);
    }

    // Called by UI to activate the Main Menu layout.
    public void SwitchToMain()
    {
        menuState = MAIN_MENU_STATE.MAIN;
        mainMenuLayout.SetActive(true);
        levelSelectLayout.SetActive(false);
        settingsLayout.SetActive(false);
        menuRot.SetRotPerSec(new Vector3(0, 20, 0));
    }

    // Called by UI to activate the Settings layout.
    public void SwitchToSettings()
    {
        menuState = MAIN_MENU_STATE.SETTINGS;
        mainMenuLayout.SetActive(false);
        levelSelectLayout.SetActive(false);
        settingsLayout.SetActive(true);
        menuRot.SetRotPerSec(new Vector3(0, 10, 0));
    }

    // Called by UI to start a level.
    // I think this became deprecated actually but I'm leaving it in just in case
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

    public void UpdateVolumeSetting()
    {
        PlayerSettings.volume = (int)volumeSlider.value;
    }

    private void Update()
    {
        if (menuState == MAIN_MENU_STATE.L_SELECT)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                SelectAdjacentLevel(-1);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                SelectAdjacentLevel(1);
            }
        }
    }
}
