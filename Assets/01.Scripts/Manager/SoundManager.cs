using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public AudioClip[] audioClips;
    public Slider bgmSlider; 
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        bgmSlider.value = 0.5f;
    }
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

        audioSource.loop = false;
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

    public void SetBgm()
    {
        audioSource.clip = GameManager.Instance.soundManager.audioClips[Json.saveData.curStage];

        audioSource.mute = !audioSource.mute;
    }
    public void SilderValue(float value)
    {
        GameManager.Instance.soundManager.audioSource.volume = value;
    }
}

