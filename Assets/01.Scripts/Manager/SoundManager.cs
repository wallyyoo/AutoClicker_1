
using UnityEngine;
/// <summary>
/// BGM 및 사운드 설정을 제어하는 사운드 매니저
/// </summary>
public class SoundManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    public AudioClip[] audioClips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void SetBGMVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}

