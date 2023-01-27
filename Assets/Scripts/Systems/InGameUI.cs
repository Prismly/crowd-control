using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    // This is here so the level knows about its target score when started directly from the editor
    [SerializeField] private int levelID;

    [SerializeField] private TextMeshProUGUI currentScoreDisp;
    [SerializeField] private TextMeshProUGUI targetScoreDisp;

    [SerializeField] private GameObject activeLayout;
    [SerializeField] private GameObject pauseLayout;
    [SerializeField] private GameObject endGameLayout;

    private void Start()
    {
        targetScoreDisp.text = LevelManager.GetLevelTargetScore(levelID).ToString();
    }

    private void Update()
    {
        currentScoreDisp.text = GameManager.GetScore().ToString();
    }
}
