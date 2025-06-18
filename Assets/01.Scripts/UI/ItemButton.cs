
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Button itemButton;
    public SoundData soundData;

    void Start()
    {
        itemButton.onClick.AddListener(OnClickItemButton);
    }

    void OnClickItemButton()
    {
        TestSoundManager.TestSoundInstance.PlaySFX(soundData);
    }
}
