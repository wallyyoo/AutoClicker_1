using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
{
    public Slider sliderBgm;
    public void OptionOpenButton(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void OptionCloseButton(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void OptionBgmButton()
    {
        if (GameManager.Instance.soundManager.audioSource.volume == 0)
        {
            GameManager.Instance.soundManager.audioSource.volume = 1;
        }
        else
        {
            GameManager.Instance.soundManager.audioSource.volume = 0;
        }
    }

    public void OptionSliderValue()
    {
        float volume = sliderBgm.value; // 0~1 사이 값
        GameManager.Instance.soundManager.SetBGMVolume(volume); // 볼륨 설정 메서드 호출
    }
}
