using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Button criticalUpButton;
    public Button attackSpeedUpButton;

    public AutoAttack autoAttack;
    public static float criticalRateTest = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        criticalUpButton.onClick.AddListener(IncreaseCriticalRate);
        attackSpeedUpButton.onClick.AddListener(DecreaseAutoAttackCooldown);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void IncreaseCriticalRate()
    {
        criticalRateTest += 0.1f; // 치명타 확률 증가
        Debug.Log("치명타 확률 증가!");
    }
    void DecreaseAutoAttackCooldown()
    { 
    autoAttack.UpgradeAttackSpeed(0.1f); // 공격 속도 증가
        Debug.Log("자동 공격 속도 상승");

    }
}
