using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    // ---- STATIC GAMEMANAGER STUFF ----

    struct LevelData
    {
        public string name;
        public float targetScore;
    }

    private static LevelData[] levelScenes;
    private const string MAIN_MENU_SCENE = "MainMenuScene";
    public static int loadedLevel = -1;

    // The theoretical maximum number of points gained per marble, if cooked optimally.
    private static float score;
    private static float scoreFactor = 100f;

    static GameManager()
    {
        LevelData lvl0;
        lvl0.name = "EmptyLevel";
        lvl0.targetScore = 500;

        LevelData lvl1;
        lvl1.name = "BoardHeatTest";
        lvl1.targetScore = 600;

        levelScenes = new LevelData[2];
        levelScenes[0] = lvl0;
        levelScenes[1] = lvl1;
    }

    // Begin a game, using the board corresponding to the given ID
    public static void LoadLevel(int levelNum)
    {
        if (levelNum >= 0 && levelNum < levelScenes.Length)
        {
            loadedLevel = levelNum;
            score = 0;
            SceneManager.LoadScene(levelScenes[levelNum].name);
        }
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }

    // Closes the game
    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ScoreMarble(float cookDegree)
    {
        // Maps cook degree to points gained using a SIN curve. An optimal cook degree of 0.5 becomes a 1, and is then multiplied by scoreFactor to reach 100.
        Debug.Log(cookDegree);
        float gainedPoints = Mathf.Sin(Mathf.PI * cookDegree) * scoreFactor;
        score += gainedPoints;
    }

    public static float GetScore()
    {
        return score;
    }

    public static float GetTargetScore()
    {
        return levelScenes[loadedLevel].targetScore;
    }
}
