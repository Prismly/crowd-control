using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuLayout;
    [SerializeField] private GameObject levelSelectLayout;

    private void Start()
    {
        if (GameManager.loadedLevel == -1)
        {
            // First load-up; boot up on main menu
            mainMenuLayout.SetActive(true);
            levelSelectLayout.SetActive(false);
        }
        else
        {
            // Subsequent load-up; boot up in level select
            mainMenuLayout.SetActive(false);
            levelSelectLayout.SetActive(true);
        }
    }

    public void SwitchToLevelSelect()
    {
        mainMenuLayout.SetActive(false);
        levelSelectLayout.SetActive(true);
    }

    public void SwitchToMain()
    {
        mainMenuLayout.SetActive(true);
        levelSelectLayout.SetActive(false);
    }

    public void LoadLevel(int levelNum)
    {
        GameManager.LoadLevel(levelNum);
    }
}
