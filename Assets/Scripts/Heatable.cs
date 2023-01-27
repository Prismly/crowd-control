using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heatable : MonoBehaviour
{
    [SerializeField] private float rawTemp;
    private float currentTemp;
    [SerializeField] private float doneTemp;

    private bool isDone = false;
    private bool isBurnt = false;

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

    // Calculate the "cook degree", a value from 0 to 1 that represents how close to being done (1.0) a Heatable is.
    // This value is used for determining the Heatable's color.
    public float CalcCookDegree()
    {
        return (currentTemp - rawTemp) / (doneTemp - rawTemp);
    }

    public void CookFood(float incVal)
    {
        // Ignore the instruction to cook the food if the food is already done, or already burnt...
        if (isDone || isBurnt)
        {
            return;
        }

        currentTemp += incVal;
        currentTemp = Mathf.Clamp(currentTemp, rawTemp, doneTemp);

        float cookDegree = CalcCookDegree();

        // Check if the degree is high enough to call this cooked. Not exactly 1, to account for float error.
        if (cookDegree > 0.99f)
        {
            isDone = true;
        }

        // Update color. We guaranteed at the start of this function that the food was not burnt, so we don't need to account for it here.
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            // Color LERPed between raw and done
            rend.material.color = Color.Lerp(rawColor, doneColor, cookDegree);
        }
    }

    public void BurnFood()
    {
        isBurnt = true;
        isDone = false;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            // Food is burnt; override with burnt color
            rend.material.color = burntColor;
        }
    }

    public bool GetIsDone()
    {
        return isDone;
    }

    public bool GetIsBurnt()
    {
        return isBurnt;
    }
}
