using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public static class LevelManager
{
    private static LevelData[] levels;
    private const string MAIN_MENU_SCENE = "MainMenuScene";

    static LevelManager()
    {
        // Initialize the Level Data, to be used until shut down.

        // LEVEL 1
        LevelData lvl1 = new LevelData("BoardHeatTest", 1000);
        // LEVEL 2
        LevelData lvl2 = new LevelData("Level2Test", 2000);

        levels = new LevelData[2];
        levels[0] = lvl1;
        levels[1] = lvl2;
    }

    public static void LoadMainScene()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    public static void PlayLevel(int levelNum)
    {
        GameManager.ResetGameVars(levelNum);
        SceneManager.LoadScene(levels[levelNum - 1].GetSceneName());
    }

    public static float GetLevelTargetScore(int levelNum)
    {
        return levels[levelNum - 1].GetTargetScore();
    }
}
