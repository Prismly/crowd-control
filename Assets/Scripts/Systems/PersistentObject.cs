using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Persistent";

        //Check if there's already a manager present
        GameObject[] persistents = GameObject.FindGameObjectsWithTag("Persistent");
        if(persistents.Length > 1)
        {
            //If so, self destruct
            Destroy(gameObject);
        }


        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
