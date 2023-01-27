using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Data structure for storing level-specific, static information. These are created and maintained by LevelManager.
public class LevelData
{
    // The name of this level's corresponding scene, so LevelManager can load that scene when needed.
    private string sceneName;

    // The minimum score required on this level to win.
    private float targetScore;

    // The currently set high score for this level.
    private float highScore;

    public LevelData(string sceneName, float targetScore)
    {
        this.sceneName = sceneName;
        this.targetScore = targetScore;
    }

    public string GetSceneName()
    {
        return sceneName;
    }

    public float GetTargetScore()
    {
        return targetScore;
    }

    public float GetHighScore()
    {
        return highScore;
    }

    public void SetHighScore(float highScore)
    {
        this.highScore = highScore;
    }
}
