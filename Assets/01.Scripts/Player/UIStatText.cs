using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatText : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerUpgradeTable playerUpgradeTable;
    [Header(" 스탯 UI")]
    public TextMeshProUGUI upCriticalRateText;
    public TextMeshProUGUI playerCriticalDamageText;
    public TextMeshProUGUI upAttackSpeedText;
    public TextMeshProUGUI upGoldGainText;

    public TextMeshProUGUI criticalUpGoldText;
    public TextMeshProUGUI autoSpeedUpGoldText;
    public TextMeshProUGUI goldGainUpGoldText;

    private void Start()
    {
        playerData = GameManager.Instance.playerData;
        playerUpgradeTable = GameManager.Instance.playerData.playerUpgradeTable;
        _upCriticalRateText();
        _playerCriticalDamageText();
        _upAttackSpeedText();
        _upGoldGainText();
        _autoSpeedUpGoldText();
        _criticalUpGoldText();
        _goldGainUpGoldText();

    }
    void _upCriticalRateText()
    {
        upCriticalRateText.text = $"치명타 확률 : +{Mathf.RoundToInt(playerUpgradeTable.critDamagePerLevel * 10)}% ";
    }
    void _playerCriticalDamageText()
    {
        playerCriticalDamageText.text = $"치명타 데미지 : {(playerData.UpStatusCriticalDamage * 100)}% ";
    }
    void _upAttackSpeedText()
    {
        upAttackSpeedText.text = $"공격속도 : -{(playerData.UpstatusAutoSpeed)}초 ";
    }
    void _upGoldGainText()
    {
        upGoldGainText.text = $"획득골드 : +{(playerUpgradeTable.goldGainPerLevel)}증가 ";
    }


    void _autoSpeedUpGoldText()
    {
        criticalUpGoldText.text = $" {playerData.UpCriticalGold}골드";
    }
    void _criticalUpGoldText()
    {
        autoSpeedUpGoldText.text = $"{playerData.UpAutoSpeedGold}골드";
    }
    void _goldGainUpGoldText()
    {
        goldGainUpGoldText.text = $" {playerData.UpGoldGainGold}골드";
    }
}
