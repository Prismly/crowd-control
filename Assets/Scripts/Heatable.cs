using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatable : MonoBehaviour
{
    [SerializeField] private float rawTemp;
    [SerializeField] private float doneTemp;
    [SerializeField] private float burntTemp;
    [SerializeField] private float currentTemp;

    // NOTE: Specifying a "keyframe" for raw, done, AND burnt means that it is possible for cooking or burning to visually appear faster. Possibly undesirable?
    private Color rawColor = new Color(253f / 255f, 155f / 255f, 154f / 255f);
    private Color doneColor = new Color(158f / 255f, 85f / 255f, 79f / 255f);
    private Color burntColor = new Color(63f / 255f, 15f / 255f, 0f / 255f);

    private void Start()
    {
        currentTemp = rawTemp;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = rawColor;
        }
    }

    // Calculate the "cook degree", a value from 0 to 1 that represents how close to the target temperature (0.5) a Heatable is.
    // This value is used for determining the Heatable's color, and how much score it awards on consumption.
    public float CalcCookDegree()
    {
        if (currentTemp < doneTemp)
        {
            return (currentTemp - rawTemp) / (doneTemp - rawTemp);
        }
        else if (currentTemp > doneTemp)
        {
            return (currentTemp - doneTemp) / (burntTemp - doneTemp);
        }
        else
        {
            return 0.5f;
        }
    }

    public void IncrementTemp(float incVal)
    {
        currentTemp += incVal;
        currentTemp = Mathf.Clamp(currentTemp, rawTemp, burntTemp);

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            float lerpVal = CalcCookDegree();

            if (currentTemp < doneTemp)
            {
                // Color LERPed between raw and done
                rend.material.color = Color.Lerp(rawColor, doneColor, lerpVal);
            }
            else
            {
                // Color LERPed between done and burnt
                rend.material.color = Color.Lerp(doneColor, burntColor, lerpVal);
            }
        }
    }
}
