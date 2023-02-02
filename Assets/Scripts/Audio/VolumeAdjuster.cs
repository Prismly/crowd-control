using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjuster : MonoBehaviour
{
    private AudioSource mySrc;

    private void Start()
    {
        mySrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        mySrc.volume = PlayerSettings.volume / 100f;
    }
}
