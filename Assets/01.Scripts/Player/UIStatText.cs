using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatText : MonoBehaviour
{
    //[HideInInspector] public PlayerData playerData;
    //[HideInInspector] public PlayerUpgradeTable playerUpgradeTable;

    //현재골드 출력 텍스트
    public TextMeshProUGUI currentGoldText;

    // 현재 스탯 출력 텍스트
    public TextMeshProUGUI currentCriticalRateText;
    public TextMeshProUGUI currentAttackSpeedText;
    public TextMeshProUGUI currentGoldGainText;

    [Header("업그레이드 증가량 안내 텍스트")]
    public TextMeshProUGUI upgradeInfoCriticalRateText;
    public TextMeshProUGUI upgradeInfoCriticalDamageText;
    public TextMeshProUGUI upgradeInfoAttackSpeedText;

    [Header("업그레이드 비용 텍스트")]
    public TextMeshProUGUI upgradeCostCriticalRateText;
    public TextMeshProUGUI upgradeCostAttackSpeedText;
    public TextMeshProUGUI upgradeCostGoldGainText;

    private void Start()
    {
        //playerData = GameManager.Instance.playerData;
        //playerUpgradeTable = GameManager.Instance.playerData.playerUpgradeTable;
        UpdateUpgradeInfoTexts();
        UpdateUpgradeCostTexts();
        UpdateCriticalRateText();
        UpdateAttackSpeedText();
        UpdateGoldGainText();
    }

    public void UpdateCriticalRateText()
    {
        var playerData = GameManager.Instance.playerData;
        float final = Mathf.Min(playerData.UpStatuscriticalChance, 1f);
        currentCriticalRateText.text = $"치명타 {Mathf.RoundToInt(final * 100)}%";
        UpdateUpgradeCostTexts();
    }

    public void UpdateAttackSpeedText()
    {
        var playerData = GameManager.Instance.playerData;
        float interval = playerData.UpstatusAutoSpeed;
        currentAttackSpeedText.text = $"자동공격 {interval:0.0}초";
        UpdateUpgradeCostTexts();
    }

    public void UpdateGoldGainText()
    {
        var playerData = GameManager.Instance.playerData;
        float total = playerData.UpStatusGold;
        currentGoldGainText.text = $"골드획득 + {total:0.0}원";
        UpdateUpgradeCostTexts();
    }
    private void UpdateUpgradeInfoTexts()
    {
        var playerData = GameManager.Instance.playerData;
        var playerUpgradeTable = playerData.playerUpgradeTable;
        upgradeInfoCriticalRateText.text = $"치명타 확률 : +{Mathf.RoundToInt(playerUpgradeTable.critDamagePerLevel * 10)}%";
        upgradeInfoCriticalDamageText.text = $"치명타 데미지 : {(playerData.UpStatusCriticalDamage * 100)}%";
        upgradeInfoAttackSpeedText.text = $"공격속도 : -{playerUpgradeTable.autoSpeedPerLevel}초";

    }

    private void UpdateUpgradeCostTexts()
    {
        var playerData = GameManager.Instance.playerData;
        upgradeCostCriticalRateText.text = $"{playerData.UpCriticalGold} 골드";
        upgradeCostAttackSpeedText.text = $"{playerData.UpAutoSpeedGold} 골드";
        upgradeCostGoldGainText.text = $"{playerData.UpGoldGainGold} 골드";
    }
}
