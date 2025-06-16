using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [Header("기본 스텟")]
    [HideInInspector] public int attackPower = 1;         //(공격력)
    [HideInInspector] public float criticalDamage = 0.1f; //(크리티컬 데미지)
    [HideInInspector] public float criticalChance = 0f;   //(크리티컬 확률)
    [HideInInspector] public float goldGain = 0f;         //(골드 획득량)
    [HideInInspector] public float autoSpeed = 1f;        // 공격속도
    [HideInInspector] public int curHp = 100;             //(현재 체력량)
    [HideInInspector] public int curMp = 100;             //(현재 마나량)

    [Header("재화 및 상태")]
    [HideInInspector] public int curGold = 0;     //(현재 가지고 있는 재화)
    [HideInInspector] public int curStage = 1;    //(현재 스테이지)

    [Header("업그레이드")]
    [HideInInspector] public int attackPowerUpLevel = 0;    //(공격력 업그레이드 수치)
    [HideInInspector] public float critiDamageUpLevel = 0f; //(크리티컬 데미지 업그레이드 수치)
    [HideInInspector] public float critiChanceUpLevel = 0f; //(크리티컬 확률 업그레이드 수치)
    [HideInInspector] public float goldGainUpLevel = 0f;    //(골드 획득량 업그레이드 수치)
    [HideInInspector] public float autoSpeedUpLevel = 0f;   //(공격속도 업그레이드 수치)
    [HideInInspector] public int hpUpLevel = 0;             //(체력량 업그레이드 수치)
    [HideInInspector] public int mpUpLevel = 0;             //(마나량 업그레이드 수치)
}