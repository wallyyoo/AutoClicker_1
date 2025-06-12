using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    [Header("기본 스텟")]
    [HideInInspector] public float attackPower;         //(공격력)
    [HideInInspector] public float criticalDamage;      //(크리티컬 데미지)
    [HideInInspector] public float criticalChance;      //(크리티컬 확률)
    [HideInInspector] public float goldGain;            //(골드 획득량)

    [Header("최종 스텟")]
    [HideInInspector] public float FinalAattackPower;   //(최종공격력)
    [HideInInspector] public float FinalCriticalDamage; //(최종 크리티컬 데미지)
    [HideInInspector] public float FinalCriticalChance; //(최종 치명타 확률)
    [HideInInspector] public float FinalGoldGain;       //(최종 골드 획득량)


}
