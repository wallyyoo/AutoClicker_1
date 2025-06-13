using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> playerAllData = new List<PlayerData>();
}
public class PlayerData
{
    [Header("기본 스텟")]
    [HideInInspector] public int attackPower;           //(공격력)
    [HideInInspector] public float criticalDamage;      //(크리티컬 데미지)
    [HideInInspector] public float criticalChance;      //(크리티컬 확률)
    [HideInInspector] public float goldGain;            //(골드 획득량)
    [HideInInspector] public float autoSpeed;           // 공격속도
    [HideInInspector] public int curHp;                 //(현재 체력량)
    [HideInInspector] public int curMp;                 //(현재 마나량)

    [Header("재화 및 상태")]
    [HideInInspector] public int curGold;               //(현재 가지고 있는 재화)
    [HideInInspector] public int curStage;              //(현재 스테이지)

    [Header("업그레이드")]
    [HideInInspector] public int attackPowerUpgrade;    //(공격력 업그레이드 수치)
    [HideInInspector] public float critiDamageUpgrade;  //(크리티컬 데미지 업그레이드 수치)
    [HideInInspector] public float critiChanceUpgrade;  //(크리티컬 확률 업그레이드 수치)
    [HideInInspector] public float goldGainUpgrade;     //(골드 획득량 업그레이드 수치)
    [HideInInspector] public float autoSpeedUpgrade;    //(공격속도 업그레이드 수치)
    [HideInInspector] public int hpUpgrade;             //(체력량 업그레이드 수치)
    [HideInInspector] public int mpUpgrade;             //(마나량 업그레이드 수치)
}