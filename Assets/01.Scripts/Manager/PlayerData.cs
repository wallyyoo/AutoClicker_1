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

    [Header("최종 스텟")]
    [HideInInspector] public int FinalAattackPower;     //(최종공격력)
    [HideInInspector] public float FinalCriticalDamage; //(최종 크리티컬 데미지)
    [HideInInspector] public float FinalCriticalChance; //(최종 치명타 확률)
    [HideInInspector] public float FinalGoldGain;       //(최종 골드 획득량)
    [HideInInspector] public int MaxHp;                 //(최대 체력량)
    [HideInInspector] public int MaxMp;                 //(최대 마나량)

    [Header("재화 및 상태")]
    [HideInInspector] public int curGold;               //(현재 가지고 있는 재화)
}
