using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZone : MonoBehaviour
{
    [SerializeField] private float heatPerSec;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Heatable heatComponent = other.gameObject.GetComponent<Heatable>();
        if (heatComponent != null)
        {
            heatComponent.IncrementTemp(heatPerSec * Time.deltaTime);
        }
    }
}
