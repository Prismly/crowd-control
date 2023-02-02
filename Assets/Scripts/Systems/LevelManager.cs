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
        LevelData lvl1 = new LevelData("Level 1", "1 - Easy Bake", 1000, PlayerPrefs.GetInt("Level1"));
        // LEVEL 2
        LevelData lvl2 = new LevelData("Level 2", "2 - Space Heater", 1500, PlayerPrefs.GetInt("Level2"));
        // LEVEL 3
        LevelData lvl3 = new LevelData("Level 3", "3 - Bake-inator", 2000, PlayerPrefs.GetInt("Level3"));

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

    public static int GetLevelHighScore(int levelNum)
    {
        return levels[levelNum - 1].GetHighScore();
    }

    public static void SetLevelHighScore(int levelNum, int newHigh)
    {
        levels[levelNum - 1].SetHighScore(newHigh);
        PlayerPrefs.SetInt("Level" + levelNum, newHigh);
        PlayerPrefs.Save();
    }

    public static int GetLevelTargetScore(int levelNum)
    {
        Debug.Log(levelNum);
        return levels[levelNum - 1].GetTargetScore();
    }

    public static string GetLevelName(int levelNum)
    {
        return levels[levelNum - 1].GetLevelName();
    }

    public static int GetLevelCount()
    {
        return levels.Length;
    }

    public static void WipeSaveData()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            SetLevelHighScore(i + 1, 0);
        }
        PlayerPrefs.SetInt("Volume", 50);
        PlayerPrefs.Save();
    }
}
