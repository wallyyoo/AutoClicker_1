using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    [HideInInspector] public PlayerUpgradeTable playerUpgradeTable;
    [Header("기본 스텟")]
    public int baseAttackPower = 1;         //(공격력)
    public float baseCriticalDamage = 2f; //(크리티컬 데미지)
    public float baseCriticalChance = 0f;   //(크리티컬 확률)
    public float baseGoldGain = 0f;         //(골드 획득량)
    public float baseAutoSpeed = 10f;        // 공격속도
    public int baseHp = 100;             //(기본 체력량)
    public int baseMp = 100;             //(기본 마나량)

    public int baseCriticalUpGold = 0;
    public int baseAutoSpeedUpGold = 0;
    public int baseGoldGainUpGold = 0;


    [Header("재화 및 상태")]
     public int curGold =0;     //(현재 가지고 있는 재화)
    
    [Header("업그레이드")]
    [HideInInspector] public int attackPowerUpLevel =0;    //(공격력 업그레이드 수치)
    [HideInInspector] public float critiDamageUpLevel = 0f; //(크리티컬 데미지 업그레이드 수치)
    [HideInInspector] public float critiChanceUpLevel = 0f; //(크리티컬 확률 업그레이드 수치)
    [HideInInspector] public float goldGainUpLevel = 1f;    //(골드 획득량 업그레이드 수치)
    [HideInInspector] public float autoSpeedUpLevel = 0f;   //(공격속도 업그레이드 수치)
    [HideInInspector] public int hpUpLevel = 0;             //(체력량 업그레이드 수치)
    [HideInInspector] public int mpUpLevel = 0;             //(마나량 업그레이드 수치)

    [HideInInspector] public int criticalGoldUpLevel = 0; 
    [HideInInspector] public int autoSpeedGoldUpLevel = 0;
    [HideInInspector] public int goldGainGoldUpLevel = 0;



    public int UpStatusAttackPower { get { return baseAttackPower + (attackPowerUpLevel * playerUpgradeTable.attackPowerPerLevel); } }
    public float UpStatusCriticalDamage { get { return baseCriticalDamage + (critiDamageUpLevel * playerUpgradeTable.critDamagePerLevel); } }
    public float UpStatuscriticalChance { get { return baseCriticalChance + (critiChanceUpLevel * playerUpgradeTable.critChancePerLevel); } }
    public float UpStatusGold { get { return baseGoldGain + (goldGainUpLevel * playerUpgradeTable.goldGainPerLevel); } }
    public float UpstatusAutoSpeed { get { return Mathf.Max(0.1f, baseAutoSpeed - (autoSpeedUpLevel * playerUpgradeTable.autoSpeedPerLevel)); } }
    public float UpstatusHp { get { return baseHp + (hpUpLevel * playerUpgradeTable.hpPerLevel); } }
    public float UpstatusMp { get { return baseMp + (mpUpLevel * playerUpgradeTable.mpPerLevel); } }
    public float UpCriticalGold { get { return baseCriticalUpGold + (critiChanceUpLevel * playerUpgradeTable.criticalGoldPerLevel); } }
    public float UpAutoSpeedGold { get { return baseAutoSpeedUpGold + (autoSpeedUpLevel * playerUpgradeTable.autospeedGoldPerLevel); } }
    public float UpGoldGainGold { get { return baseGoldGainUpGold + (goldGainUpLevel * playerUpgradeTable.goldGainGoldPerLevel); } }
}
