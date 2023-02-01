using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSunDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Shader.SetGlobalVector("_SunDirection", transform.forward);
    }

    private void Update()
    {
        Shader.SetGlobalVector("_SunDirection", transform.forward);
    }
}
