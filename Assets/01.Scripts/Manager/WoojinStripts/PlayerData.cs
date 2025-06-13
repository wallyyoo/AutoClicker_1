using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataList
{
    public List<PlayerData> playerAllData = new List<PlayerData>();
}
[System.Serializable]
public class PlayerData
{
    [Header("기본 스텟")]
    [HideInInspector] public int attackPower = 1;         //(공격력)
    [HideInInspector] public float criticalDamage = 1f;   //(크리티컬 데미지)
    [HideInInspector] public float criticalChance = 1f;   //(크리티컬 확률)
    [HideInInspector] public float goldGain = 1f;         //(골드 획득량)
    [HideInInspector] public float autoSpeed = 1f;        // 공격속도
    [HideInInspector] public int curHp = 100;             //(현재 체력량)
    [HideInInspector] public int curMp = 100;             //(현재 마나량)

    [Header("재화 및 상태")]
    [HideInInspector] public int curGold = 0;     //(현재 가지고 있는 재화)
    [HideInInspector] public int curStage = 1;    //(현재 스테이지)

    [Header("업그레이드")]
    [HideInInspector] public int attackPowerUpgrade = 0;    //(공격력 업그레이드 수치)
    [HideInInspector] public float critiDamageUpgrade = 0f; //(크리티컬 데미지 업그레이드 수치)
    [HideInInspector] public float critiChanceUpgrade = 0f; //(크리티컬 확률 업그레이드 수치)
    [HideInInspector] public float goldGainUpgrade = 0f;    //(골드 획득량 업그레이드 수치)
    [HideInInspector] public float autoSpeedUpgrade = 0f;   //(공격속도 업그레이드 수치)
    [HideInInspector] public int hpUpgrade = 0;             //(체력량 업그레이드 수치)
    [HideInInspector] public int mpUpgrade = 0;             //(마나량 업그레이드 수치)
}