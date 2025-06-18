using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    public GameObject creditPanel;
    public GameObject settingPanel;
    public SoundData SelectedSound;
    
    public void OnClickStartButton()              // 타이틀 씬에서 "게임시작" 버튼 클릭
    {
        TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
        SceneManager.LoadScene("01.MainScene");
    }
    
    public void OnClickReStartButton()            // 타이틀 씬에서 "이어하기" 버튼 클릭
    {
        TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
        SceneManager.LoadScene("01.MainScene");
    }
    
    public void OnClickCreditButton()             // 타이틀 씬에서 "크레딧" 버튼 클릭
    {
        TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
        creditPanel.SetActive(true);
        Debug.Log("크레딧 버튼 눌림");
    }
    
    public void OnClickSettingButton()            // 타이틀 씬에서 "설정" 버튼 클릭
    {
        TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
        settingPanel.SetActive(true);
        Debug.Log("셋팅 버튼 눌림");
    }
    
    public void OnClickExitButton()               // 타이틀 씬에서 "게임종료" 버튼 클릭
    {
        TestSoundManager.TestSoundInstance.PlaySFX(SelectedSound);
        Application.Quit();
        Debug.Log("게임종료 버튼 눌림 : 빌드에서만 게임종료가 됨");
    }

    public void OnClickCloseButton()              // 타이틀 씬에서 "닫기" 버튼 클릭 
    {
        
    }
}
