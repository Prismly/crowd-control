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
        LevelData lvl1 = new LevelData("Level 1", 2000);
        // LEVEL 2
        LevelData lvl2 = new LevelData("Level 2", 2000);
        // LEVEL 3
        LevelData lvl3 = new LevelData("Level 3", 2000);

        levels = new LevelData[3];
        levels[0] = lvl1;
        levels[1] = lvl2;
        levels[2] = lvl3;
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

    public static int GetLevelTargetScore(int levelNum)
    {
        Debug.Log(levelNum);
        return levels[levelNum - 1].GetTargetScore();
    }
}
