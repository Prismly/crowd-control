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
        bool objectArraysAreValid = (RawObjects.Length == CookedObjects.Length &&
                                    RawObjects.Length == FoodRotations.Length &&
                                    RawObjects.Length == FoodScales.Length &&
                                    RawObjects.Length != 0);
        Assert.IsTrue(objectArraysAreValid);

        FoodIdx = (int)Random.Range(0, RawObjects.Length);

        SwapFoodModel(RawObjects);

        heatComp = GetComponent<Heatable>();
        Assert.IsNotNull(heatComp);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < DespawnHeightY)
        {
            // Marble is sufficiently low enough to be eaten; send its cooked-ness to the Game Manager for scoring and destroy it
            GameManager.ScoreMarble(heatComp);
            Destroy(gameObject);
        }

        if (!becameCooked && heatComp.GetIsDone())
        {
           SwapFoodModel(CookedObjects);
           becameCooked = true;
        }
    }
}
