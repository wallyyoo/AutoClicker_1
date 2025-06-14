using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager uiInstance;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            if (uiInstance == null)
            {
                uiInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                GameManager.Instance.DebugLog("uiMamager is null");
                Destroy(gameObject);
            }
        }
    }

    public TextMeshProUGUI curGoldText;

    // Start is called before the first frame update
    void Start()
    {
        OnUI();
    }

    public void OnUI()
    {
        curGoldText.text = GameManager.Instance.playerData.curGold.ToString();
    }
    public void OptionCloseButton(GameObject buttonObj)
    {
        GameManager.Instance.DebugLog("버튼 클릭");
        buttonObj.SetActive(false);
    }
}
