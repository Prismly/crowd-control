using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    // The number of the level currently being played.
    public static int loadedLevelID = -1;

    // The theoretical maximum number of points gained per marble, if cooked optimally.
    private static float score = 0;
    private static float doneScore = 100f;
    private static float burntScore = -25f;
    private static float rawScore = -5f;

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
    }

    public static float GetScore()
    {
        return score;
    }

    // Clean up all variables, such that the GameManager ready to start a new level.
    public static void ResetGameVars(int levelID)
    {
        loadedLevelID = levelID;
        score = 0;
    }
}
