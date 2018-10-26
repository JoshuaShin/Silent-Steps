using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixerSnapshot volumeDown;           //Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
    public AudioMixerSnapshot volumeUp;             //Reference to Audio mixer snapshot in which the master volume of main mixer is turned up
    private AudioSource musicSource;                //Reference to the AudioSource which plays music
    private AudioSource sfxSource;                  //Reference to the AudioSource which plays sfx
    private float resetTime = .01f;					//Very short time used to fade in near instantly without a click
    private float transitionTime = .1f;
    private float volLowRangeSfx = .4f;
    private float volHighRangeSfx = 1f;

    // BGM
    [Header("BGM")]
    public AudioClip titleBgm;                    //Assign Audioclip for title music loop
    public AudioClip mainBgm;                     //Assign Audioclip for main

    // SFX
    [Header("SFX")]
    public AudioClip[] footstepSfx;
    public AudioClip[] footstepWomanSfx;
    public AudioClip[] footstepWetSfx;
    public AudioClip clickSfx;
    public AudioClip doorSfx;

    public static SoundManager instance = null;
    
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Get a component reference to the AudioSource attached to the UI game object
        musicSource = GetComponents<AudioSource>()[0];
        sfxSource = GetComponents<AudioSource>()[1];
    }


    // BGM //

    public void PlayMapMusic()
    {
        StartCoroutine(PlayMapMusicCoroutine());
    }

    IEnumerator PlayMapMusicCoroutine()
    {
        FadeDown(transitionTime);
        yield return new WaitForSeconds(transitionTime);

        musicSource.clip = mainBgm;
        musicSource.Play();

        FadeUp(resetTime);
    }


    // SFX //

    public void PlayFootstepSfx()
    {
        sfxSource.PlayOneShot(footstepSfx[Random.Range(0, footstepSfx.Length - 1)], RandomVolume() * 2);
    }

    public void PlayDoorSfx()
    {
        sfxSource.PlayOneShot(doorSfx, RandomVolume());
    }

    public void PlayClickSfx()
    {
        sfxSource.PlayOneShot(clickSfx, RandomVolume());
    }


    // UTILITY //

    // Call this function to very quickly fade up the volume of master mixer
    public void FadeUp(float fadeTime)
    {
        // Call the TransitionTo function of the audioMixerSnapshot volumeUp;
        volumeUp.TransitionTo(fadeTime);
    }

    // Call this function to fade the volume to silence over the length of fadeTime
    public void FadeDown(float fadeTime)
    {
        // Call the TransitionTo function of the audioMixerSnapshot volumeDown;
        volumeDown.TransitionTo(fadeTime);
    }

    private float RandomVolume()
    {
        return Random.Range(volLowRangeSfx, volHighRangeSfx);
    }
}
