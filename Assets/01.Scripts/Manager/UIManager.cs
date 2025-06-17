using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public AudioSource bgmSource;

    public void OptionButton(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void Close(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void SetBgmButton()
    {
        GameManager.Instance.soundManager.SetBgm();
    }

    private IEnumerator JsonSaveCoroutine()
    {
        yield return null; // 모든 Awake() 완료 대기

        if (Json.saveData == null)
        {
            Debug.Log("null");
            Json.JsonSave();
        }
    }
}
