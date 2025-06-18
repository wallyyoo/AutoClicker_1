using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource SfxSource;

    public AudioClip[] SfxCilps;
    public AudioClip[] audioClips;

    public void Bgm(int _index)
    {
        if (audioClips == null || audioClips.Length <= _index)
        {
            Debug.LogWarning("유효하지 않은 BGM 인덱스입니다.");
            return;
        }

        if (audioSource.clip == audioClips[_index])
        {
            Debug.Log("이미 재생 중인 BGM입니다.");
            return;
        }

        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.panStereo = 0;

        audioSource.clip = audioClips[_index];
        audioSource.Play();
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

    public void SilderVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}

