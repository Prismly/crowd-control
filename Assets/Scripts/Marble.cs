using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    public float DespawnHeightY = -10f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.IncFoodCount(1);
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
    }
}
