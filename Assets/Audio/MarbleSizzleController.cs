using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleSizzleController : MonoBehaviour
{
    public AudioClip SizzleRaw;
    public AudioClip SizzleCooked;
    public AudioClip SizzleBurned;
    public float CookedSoundThreshold;
    public float PitchAdjustMin;
    public float PitchAdjustMax;
    public float VolAdjustMin;
    public float VolAdjustMax;
    public float CooldownSeconds;
    private float CooldownProgress;

    private AudioSource audioSrc;


    void PlayAudioClip(Heatable heatComp)
    {
        //if we're cooked enough or burned enough to switch sounds
        if(heatComp.CalcCookDegree() > CookedSoundThreshold || heatComp.CalcBurnDegree() > CookedSoundThreshold)
        {
            //If we're burned, or at least more burned than cooked
            if(heatComp.GetIsBurnt() || heatComp.CalcBurnDegree() > heatComp.CalcCookDegree())
            {
                audioSrc.clip = SizzleBurned;
            }
            else
            {
                audioSrc.clip = SizzleCooked;
            }
        }
        else
        {
            audioSrc.clip = SizzleRaw;
        }
        audioSrc.Play();
        CooldownProgress = CooldownSeconds;
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();

        CookedSoundThreshold = Mathf.Clamp(CookedSoundThreshold, 0f, 1f);

        float audioPitchAdjust = Random.Range(PitchAdjustMin, PitchAdjustMax);
        float audioVolAdjust = Random.Range(VolAdjustMin, VolAdjustMax);
        audioSrc.pitch += audioPitchAdjust;
        audioSrc.pitch = Mathf.Clamp(audioSrc.pitch, -3f, 3f);
        audioSrc.volume += audioVolAdjust;
        audioSrc.volume = Mathf.Clamp(audioSrc.volume, 0f, 1f);

        CooldownProgress = 0f;
    }

    void Update()
    {
        CooldownProgress -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(CooldownProgress <= 0f && collision.gameObject.tag == "Marble")
        {
            PlayAudioClip(collision.gameObject.GetComponent<Heatable>());
        }
    }
}
