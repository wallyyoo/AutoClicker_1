using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [Header("기본 스텟")]
    [SerializeField] private int attackPower = 1;         //(공격력)
    [SerializeField] private float criticalDamage = 0.1f; //(크리티컬 데미지)
    [SerializeField] private float criticalChance = 0f;   //(크리티컬 확률)
    [SerializeField] private float goldGain = 0f;         //(골드 획득량)
    [SerializeField] private float autoSpeed = 1f;        // 공격속도
    [SerializeField] private int curHp = 100;             //(현재 체력량)
    [SerializeField] private int curMp = 100;             //(현재 마나량)

    [Header("재화 및 상태")]
    [HideInInspector] public int curGold = 0;     //(현재 가지고 있는 재화)

    [Header("업그레이드")]
    [HideInInspector] public int attackPowerUpLevel = 0;    //(공격력 업그레이드 수치)
    [HideInInspector] public float critiDamageUpLevel = 0f; //(크리티컬 데미지 업그레이드 수치)
    [HideInInspector] public float critiChanceUpLevel = 0f; //(크리티컬 확률 업그레이드 수치)
    [HideInInspector] public float goldGainUpLevel = 0f;    //(골드 획득량 업그레이드 수치)
    [HideInInspector] public float autoSpeedUpLevel = 0f;   //(공격속도 업그레이드 수치)
    [HideInInspector] public int hpUpLevel = 0;             //(체력량 업그레이드 수치)
    [HideInInspector] public int mpUpLevel = 0;             //(마나량 업그레이드 수치)

    public int UpStatusAttackPower => attackPower + (attackPowerUpLevel * 5);
    public float UpStatusCriticalDamage => criticalDamage + (critiDamageUpLevel * 0.5f);
    public float UpStatuscriticalChance => criticalChance + (critiChanceUpLevel * 0.2f);
    public float UpStatusGold => goldGain + (goldGainUpLevel * 0.5f);
    public float UpstatusAutoSpeed => autoSpeed + (autoSpeedUpLevel * 0.5f);
    public float UpstatusHp => curHp + (hpUpLevel * 100);
    public float UpstatusMp => curMp + (mpUpLevel * 100);
}