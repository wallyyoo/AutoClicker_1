using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSoundManager : MonoBehaviour
{
  public static TestSoundManager TestSoundInstance;
  
  [Header("사운드 소스")]
  public AudioSource bgmSource;
  public AudioSource sfxSource;


  void Awake()
  {
    if (TestSoundInstance == null)
    {
      TestSoundInstance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void PlayBGM(SoundData bgmData)
  {
    if (bgmData == null || bgmData.clip == null) 
    {
      Debug.LogWarning("BGM이 비어있습니다.");
      return;
    }
    
    bgmSource.clip = bgmData.clip;
    bgmSource.volume = bgmData.volume;
    bgmSource.pitch = bgmData.pitch;
    bgmSource.loop = bgmData.loop;
    bgmSource.Play();
    
    Debug.Log($"✅ BGM 재생 시작: {bgmData.clip.name}");
  }

  public void PlaySFX(SoundData sfxData)
  {
    if (sfxData == null || sfxData.clip == null)
    {
      Debug.LogWarning("Clip이 비어있습니다.");
      return;
    }
    
    sfxSource.PlayOneShot(sfxData.clip, sfxData.volume);
  }

  public void StopBGM()
  {
    bgmSource.Stop();
  }
}
