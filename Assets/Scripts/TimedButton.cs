using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimedButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI btnText;

    [SerializeField] [TextArea] private string baseText;
    [SerializeField] [TextArea] private string askingText;
    [SerializeField] [TextArea] private string confirmText;
    [SerializeField] [TextArea] private string deconfirmText;

    [SerializeField] private float askingTime;
    [SerializeField] private float resultTime;
    private float waitProg = 0f;

    private ConfirmState myState = ConfirmState.IDLE;

    private enum ConfirmState
    {
        IDLE,
        ASKING,
        CONFIRMED,
        DECONFIRMED
    }

    private void Update()
    {
        if (myState != ConfirmState.IDLE)
        {
            waitProg += Time.deltaTime;

            if (myState == ConfirmState.ASKING && waitProg > askingTime)
            {
                // Waiting is done. Deconfirm
                myState = ConfirmState.DECONFIRMED;
                waitProg = 0f;
                btnText.text = deconfirmText;
            }
            else if (waitProg > resultTime)
            {
                // Waiting is done. Reset to base
                myState = ConfirmState.IDLE;
                waitProg = 0f;
                btnText.text = baseText;
            }
        }
    }

    public void TimedClick()
    {
        if (myState == ConfirmState.ASKING)
        {
            // CONFIRMATION CLICK
            myState = ConfirmState.CONFIRMED;
            waitProg = 0f;
            btnText.text = confirmText;

            // And also actually do the action'
            ConfirmedFunction();
        }
        else if (myState == ConfirmState.IDLE)
        {
            // ASK FOR CONFIRMATION
            myState = ConfirmState.ASKING;
            waitProg = 0f;
            btnText.text = askingText;
        }
    }

    public void ConfirmedFunction()
    {
        LevelManager.WipeSaveData();
    }
}
