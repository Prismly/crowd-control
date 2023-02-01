using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChompHitbox : MonoBehaviour
{
    [SerializeField] SlimeAnimationManager slimeAnimMan;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Marble")
        {
            // A marble is in range! Trigger CHOMP
            slimeAnimMan.ChompAnim();
        }
    }
}
