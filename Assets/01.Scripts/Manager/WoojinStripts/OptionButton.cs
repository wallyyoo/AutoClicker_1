using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
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
}
