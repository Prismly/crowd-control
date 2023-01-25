using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreDisp;
    [SerializeField] private TextMeshProUGUI targetScoreDisp;

    private void Start()
    {
        targetScoreDisp.text = GameManager.GetTargetScore().ToString();
    }

    private void Update()
    {
        currentScoreDisp.text = GameManager.GetScore().ToString();
    }
}