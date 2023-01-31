using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationPerSec;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.Rotate(rotationPerSec * Time.deltaTime, Space.Self);
    }
}
