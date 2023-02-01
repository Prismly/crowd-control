using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data structure for storing level-specific, static information. These are created and maintained by LevelManager.
public class LevelData
{
    // The name of this level's corresponding scene, so LevelManager can load that scene when needed.
    private string sceneName;

    private string levelName;

    // The minimum score required on this level to win.
    private int targetScore;

    // The currently set high score for this level.
    private int highScore = 0;

    public LevelData(string sceneName, string levelName, int targetScore, int highScore)
    {
        this.sceneName = sceneName;
        this.levelName = levelName;
        this.targetScore = targetScore;
        this.highScore = highScore;
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    public int GetTargetScore()
    {
        return targetScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public string GetLevelName()
    {
        return levelName;
    }

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }
}
