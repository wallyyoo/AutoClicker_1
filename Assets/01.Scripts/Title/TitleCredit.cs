using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCredit : MonoBehaviour
{
   public GameObject creditPanel;
   public SoundData SelectedSound;
   
   public void OnClickCreditCloseButton()
   {
      TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
      creditPanel.SetActive(false);
   }
}
