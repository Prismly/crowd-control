using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data structure for storing level-specific, static information. These are created and maintained by LevelManager.
public class LevelData
{
    // The name of this level's corresponding scene, so LevelManager can load that scene when needed.
    private string sceneName;

    // The minimum score required on this level to win.
    private int targetScore;

    // The currently set high score for this level.
    private int highScore;

    public LevelData(string sceneName, int targetScore)
    {
        this.sceneName = sceneName;
        this.targetScore = targetScore;
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

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }
}
