using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    // The number of the level currently being played.
    public static int loadedLevelID = -1;

    // The theoretical maximum number of points gained per marble, if cooked optimally.
    private static int score = 0;
    private static int doneScore = 100;
    private static int burntScore = -25;
    private static int rawScore = -5;

    private static int spawnersActive = 0;
    private static int foodInPlay = 0;

    public static InGameUI gameUIManager;
    public static void ScoreMarble(Heatable heatComp)
    {
        if (heatComp.GetIsDone() && !heatComp.GetIsBurnt())
        {
            // Well cooked. Award max points!!
            score += doneScore;
        }
        else if (heatComp.GetIsBurnt())
        {
            // Burnt. Big point deduction!
            score += burntScore;
        }
        else
        {
            // Somewhat raw. Small point deduction...
            score += rawScore;
        }

        // Remove 1 from the total Food Count.
        IncFoodCount(-1);

        if (spawnersActive == 0 & foodInPlay == 0)
        {
            // This was the last marble. Check for a win.
            if (score >= LevelManager.GetLevelTargetScore(loadedLevelID))
            {
                // Score has passed target value. WIN!!!!!
                // --- WIN ---
                gameUIManager.Win();
            }
            else
            {
                // Score is below target value. LOSE...
                // --- LOSE ---
                gameUIManager.Lose();
            }
        }
    }

    public static int GetScore()
    {
        return score;
    }

    public static int GetLevelID()
    {
        return loadedLevelID;
    }

    // Clean up all variables, such that the GameManager ready to start a new level.
    public static void ResetGameVars(int levelID)
    {
        loadedLevelID = levelID;
        score = 0;
    }

    public static void IncSpawnerCount(int incVal)
    {
        spawnersActive += incVal;
        // Ensure that the Spawner Count never goes below 0.
        spawnersActive = Mathf.Clamp(spawnersActive, 0, spawnersActive);
    }

    public static void IncFoodCount(int incVal)
    {
        foodInPlay += incVal;
        // Ensure that the Food Count never goes below 0.
        foodInPlay = Mathf.Clamp(foodInPlay, 0, foodInPlay);
    }
}
