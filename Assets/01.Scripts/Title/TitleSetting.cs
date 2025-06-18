using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSetting : MonoBehaviour
{
    public GameObject settingPanel;
    public SoundData SelectedSound;
    public void OnClickCreditCloseButton()
    {
        TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
        settingPanel.SetActive(false);
    }
}
