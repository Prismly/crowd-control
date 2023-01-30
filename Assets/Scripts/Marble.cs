using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Marble : MonoBehaviour
{
    public float DespawnHeightY = -10f;
    public GameObject[] RawObjects;
    public GameObject[] CookedObjects;
    public Vector3[] FoodRotations;
    public Vector3[] FoodScales;
    public GameObject cookedFX;
    public Vector3 cookedFXPositionOffset;

    private GameObject Food;
    private int FoodIdx = -1;

    private Heatable heatComp;

    private bool becameCooked = false;

    private void SwapFoodModel(GameObject[] ModelList)
    {
        if (Food != null)
        {
            Destroy(Food);
        }
        Food = Instantiate(ModelList[FoodIdx], transform.position, transform.rotation);
        Food.transform.rotation *= Quaternion.Euler(FoodRotations[FoodIdx]);
        Food.transform.parent = transform;
        Food.transform.localScale = FoodScales[FoodIdx];
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.IncFoodCount(1);

        bool objectArraysAreValid = (RawObjects.Length == CookedObjects.Length &&
                                    RawObjects.Length == FoodRotations.Length &&
                                    RawObjects.Length == FoodScales.Length &&
                                    RawObjects.Length != 0);
        Assert.IsTrue(objectArraysAreValid);

        FoodIdx = (int)Random.Range(0, RawObjects.Length);

        SwapFoodModel(RawObjects);

        //Apply random Z rotation to fresh spawns
        float randRot = Random.Range(0f, 360f);
        transform.rotation *= Quaternion.Euler(new Vector3(0f, 0f, randRot));

        heatComp = GetComponent<Heatable>();
        Assert.IsNotNull(heatComp);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < DespawnHeightY)
        {
            // Marble is sufficiently low enough to be eaten; send its cooked-ness to the Game Manager for scoring and destroy it

            Heatable heatComp = GetComponent<Heatable>();
            // Update the score AND check if the level is over as a result of this marble being consumed
            GameManager.ScoreMarble(heatComp);

            // Destroy the marble object
            Destroy(gameObject);
        }

        if (!becameCooked && heatComp.GetIsDone())
        {
            SwapFoodModel(CookedObjects);
            if(cookedFX != null)
            {
                Instantiate(cookedFX, transform.position + cookedFXPositionOffset, Quaternion.identity);
            }
            becameCooked = true;
        }
    }
}
