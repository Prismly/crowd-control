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

    //Particles
    //Cook
    public GameObject cookedFX;
    private GameObject cookedFXInstance;
    public GameObject partialCookedFX;
    private GameObject partialCookedFXInstance;
    public int partialCookedFXMaxParticles;
    public AnimationCurve partialCookedFXParticleDensityCurve;
    //Burn
    public GameObject burnedFX;
    private GameObject burnedFXInstance;
    public GameObject partialBurnedFX;
    private GameObject partialBurnedFXInstance;
    public int partialBurnedFXMaxParticles;
    public AnimationCurve partialBurnedFXParticleDensityCurve;
    //Offset
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

    public void DestroyMarble()
    {
        //Marble is being eaten or burned. Send its cooked-ness to the Game Manager for scoring and destroy it
        Heatable heatComp = GetComponent<Heatable>();
        // Update the score AND check if the level is over as a result of this marble being consumed
        GameManager.ScoreMarble(heatComp);

        // Destroy the marble object
        Destroy(gameObject);
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
            // Marble is sufficiently low enough to be eaten;
            DestroyMarble();
        }

        if(heatComp.GetIsBurnt())
        {
            burnedFXInstance = Instantiate(burnedFX, transform.position + cookedFXPositionOffset, Quaternion.identity);
            DestroyMarble();
        }
        else
        {
            if(heatComp.CalcBurnDegree() > 0f)
            {
                if(partialBurnedFXInstance == null)
                {
                    partialBurnedFXInstance = Instantiate(partialBurnedFX, transform.position + cookedFXPositionOffset, Quaternion.identity);
                    partialBurnedFXInstance.transform.parent = transform;
                }
                else
                {
                    //Increase number of particles based on how burned we are
                    ParticleSystem instParticleSystem = partialBurnedFXInstance.GetComponent<ParticleSystem>();
                    var instParticleSystemMain = instParticleSystem.main;
                    instParticleSystemMain.maxParticles = (int)(partialBurnedFXMaxParticles * partialBurnedFXParticleDensityCurve.Evaluate(heatComp.CalcBurnDegree()));
                }
            }
        }

        if (!becameCooked)
        {
            if(heatComp.CalcCookDegree() > 0f)
            {
                if(partialCookedFXInstance == null)
                {
                    partialCookedFXInstance = Instantiate(partialCookedFX, transform.position + cookedFXPositionOffset, Quaternion.identity);
                    partialCookedFXInstance.transform.parent = transform;
                }
                else
                {
                    //Increase number of particles based on how cooked we are
                    ParticleSystem instParticleSystem = partialCookedFXInstance.GetComponent<ParticleSystem>();
                    var instParticleSystemMain = instParticleSystem.main;
                    instParticleSystemMain.maxParticles = (int)(partialCookedFXMaxParticles * partialCookedFXParticleDensityCurve.Evaluate(heatComp.CalcCookDegree()));
                }
            }
            if(heatComp.GetIsDone())
            {
                SwapFoodModel(CookedObjects);
                if(cookedFX != null)
                {
                    cookedFXInstance = Instantiate(cookedFX, transform.position + cookedFXPositionOffset, Quaternion.identity);
                }
                if(partialCookedFXInstance != null)
                {
                    Destroy(partialCookedFXInstance);
                }

                becameCooked = true;
            }
        }
    }
}
