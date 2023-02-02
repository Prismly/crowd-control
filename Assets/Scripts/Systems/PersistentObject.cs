using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    public string UniqueTag;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = UniqueTag;

        //Check if there's already a persistent object w/ this tag present
        GameObject[] persistents = GameObject.FindGameObjectsWithTag(UniqueTag);
        if(persistents.Length > 1)
        {
            //If so, self destruct
            Destroy(gameObject);
        }

        //Otherwise, persist
        DontDestroyOnLoad(gameObject);
    }
}
