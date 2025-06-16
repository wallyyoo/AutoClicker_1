using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData_1
{
    public PlayerUpgradeTable playerUpgradeTable;
    [Header("기본 스텟")]
    [SerializeField] private int baseAttackPower = 1;         //(공격력)
    [SerializeField] private float baseCriticalDamage = 2f; //(크리티컬 데미지)
    [SerializeField] private float baseCriticalChance = 0f;   //(크리티컬 확률)
    [SerializeField] private float baseGoldGain = 0f;         //(골드 획득량)
    [SerializeField] private float baseAutoSpeed = 500f;        // 공격속도
    [SerializeField] private int baseHp = 100;             //(기본 체력량)
    [SerializeField] private int baseMp = 100;             //(기본 마나량)

    [Header("재화 및 상태")]
    [HideInInspector] public int curGold = 0;     //(현재 가지고 있는 재화)
    [HideInInspector] public int curStage = 1;    //(현재 스테이지)

    [Header("업그레이드")]
    [HideInInspector] public int attackPowerUpLevel =0;    //(공격력 업그레이드 수치)
    [HideInInspector] public float critiDamageUpLevel = 0f; //(크리티컬 데미지 업그레이드 수치)
    [HideInInspector] public float critiChanceUpLevel = 0f; //(크리티컬 확률 업그레이드 수치)
    [HideInInspector] public float goldGainUpLevel = 0f;    //(골드 획득량 업그레이드 수치)
    [HideInInspector] public float autoSpeedUpLevel = 0f;   //(공격속도 업그레이드 수치)
    [HideInInspector] public int hpUpLevel = 0;             //(체력량 업그레이드 수치)
    [HideInInspector] public int mpUpLevel = 0;             //(마나량 업그레이드 수치)

    public int UpStatusAttackPower { get { return baseAttackPower + (attackPowerUpLevel * playerUpgradeTable.attackPowerPerLevel); } }
    public float UpStatusCriticalDamage { get { return baseCriticalDamage + (critiDamageUpLevel * playerUpgradeTable.critDamagePerLevel); } }
    public float UpStatuscriticalChance { get { return baseCriticalChance + (critiChanceUpLevel * playerUpgradeTable.critChancePerLevel); } }
    public float UpStatusGold { get { return baseGoldGain + (goldGainUpLevel * playerUpgradeTable.goldGainPerLevel); } }
    public float UpstatusAutoSpeed { get { return Mathf.Max(0.1f, baseAutoSpeed - (autoSpeedUpLevel * playerUpgradeTable.autoSpeedPerLevel)); } }
    public float UpstatusHp { get { return baseHp + (hpUpLevel * playerUpgradeTable.hpPerLevel); } }
    public float UpstatusMp { get { return baseMp + (mpUpLevel * playerUpgradeTable.mpPerLevel); } }
}