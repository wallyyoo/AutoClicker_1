
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 옵션 메뉴 열기/닫기 토글 기능을 담당하는 클래스
/// </summary>
public class OptionButton : MonoBehaviour
{
    Slider sliderBgm;
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
