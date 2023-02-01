using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobbing : MonoBehaviour
{
    private RectTransform myRect;
    private Vector3 homePos;

    [SerializeField] private float cycleTime;
    [SerializeField] private float cycleAmp;
    [SerializeField] private float cycleOffset;
    private float cycleProg = 0f;

    private void Start()
    {
        myRect = GetComponent<RectTransform>();
        homePos = myRect.localPosition;
    }

    private void Update()
    {
        float cycleSpeed = 2 * Mathf.PI / cycleTime;
        cycleProg += cycleSpeed * Time.deltaTime;

        if (cycleProg > 2 * Mathf.PI)
        {
            cycleProg -= 2 * Mathf.PI;
        }

        float vertOffset = Mathf.Sin(cycleProg) * cycleAmp;
        myRect.localPosition = homePos + (Vector3.up * vertOffset);
    }
}
