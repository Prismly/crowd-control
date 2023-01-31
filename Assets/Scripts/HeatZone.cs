using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatZone : MonoBehaviour
{
    [SerializeField] private float heatPerSec;
    [SerializeField] private float burnPerSec;
    [SerializeField] private bool IsBurnZone = false;

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
            if(IsBurnZone)
            {
                heatComponent.BurnFood(burnPerSec * Time.deltaTime);
            }
            else
            {
                heatComponent.CookFood(heatPerSec * Time.deltaTime);
            }
        }
    }
}
