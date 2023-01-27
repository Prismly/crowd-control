using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    public float DespawnHeightY = -10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < DespawnHeightY)
        {
            // Marble is sufficiently low enough to be eaten; send its cooked-ness to the Game Manager for scoring and destroy it
            Heatable heatComp = GetComponent<Heatable>();
            if (heatComp != null)
            {
                GameManager.ScoreMarble(heatComp);
            }
            Destroy(gameObject);
        }
    }
}
