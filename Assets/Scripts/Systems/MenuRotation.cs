using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotationPerSec;
    [SerializeField] private Vector3Int rotationIsNegative;
    [SerializeField] private bool slowingDown = false;
    [SerializeField] private float slowDownRate = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        if (slowingDown)
        {
            // Slow down rotation speed, ensuring none of the axes go below 0
            Vector3 overkill = rotationPerSec - Vector3.one * slowDownRate;
            overkill.x = overkill.x < 0 ? 0 : overkill.x;
            overkill.y = overkill.y < 0 ? 0 : overkill.y;
            overkill.z = overkill.z < 0 ? 0 : overkill.z;

            rotationPerSec -= (overkill + (Vector3.one * slowDownRate)) * Time.deltaTime;
            rotationPerSec.x = rotationPerSec.x < 0 ? 0 : rotationPerSec.x;
            rotationPerSec.y = rotationPerSec.y < 0 ? 0 : rotationPerSec.y;
            rotationPerSec.z = rotationPerSec.z < 0 ? 0 : rotationPerSec.z;

            if (rotationPerSec.Equals(Vector3.zero))
            {
                // We're done slowing down, flag as such
                slowingDown = false;
            }
        }

        Vector3 realRotation = new Vector3(rotationPerSec.x * rotationIsNegative.x, rotationPerSec.y * rotationIsNegative.y, rotationPerSec.z * rotationIsNegative.z);
        gameObject.transform.Rotate(realRotation * Time.deltaTime, Space.Self);
    }

    public void SwivelAndSlow(Vector3 swivelStartSpeed)
    {
        rotationPerSec += swivelStartSpeed;
        slowingDown = true;
    }

    public void SetRotPerSec(Vector3 newRot)
    {
        rotationPerSec = newRot;
        slowingDown = false;
    }

    public void SetRotIsNegative(Vector3Int newRotSigns)
    {
        rotationIsNegative = newRotSigns;
    }
}
