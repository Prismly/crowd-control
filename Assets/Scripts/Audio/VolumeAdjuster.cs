using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjuster : MonoBehaviour
{
    private AudioSource mySrc;
    [SerializeField] private float volumeFactor = 1f;
    private void Start()
    {
        mySrc = GetComponent<AudioSource>();
    }

    private void Update()
    {
        mySrc.volume = PlayerSettings.volume / 100f * volumeFactor;
    }
}
