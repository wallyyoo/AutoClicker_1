using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBgm(AudioClip clips)
    {
        if (audioSource.clip == clips)
        {
            return;
        }

        audioSource.clip = clips;
        audioSource.Play();
    }
    public void startBgm()
    {
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.panStereo = 0;
    }
}
