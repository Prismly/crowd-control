using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string[] levelScenes = { "EmptyLevel", "Level01" };

    // Begin a game, using the board corresponding to the given ID
    public void LoadLevel(int levelNum)
    {
        SceneManager.LoadScene(levelScenes[levelNum]);
    }
}
