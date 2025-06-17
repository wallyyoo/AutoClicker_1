using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public void OptionOpenButton(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void CloseButton(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void BgmCheckButton()
    {
        GameManager.Instance.soundManager.SetBgm();
    }

    // 이 함수를 fillamount로 바꿔야함
    //public void SilderChange()
    //{
    //    GameManager.Instance.soundManager.SilderValue(GameManager.Instance.soundManager.bgmSlider.value);
    //}
}
