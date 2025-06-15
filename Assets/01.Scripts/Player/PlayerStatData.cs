using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatData", menuName = "GameData/PlayerStatData")]
public class PlayerStatData : ScriptableObject
{
    [Header("기본 스텟")]
    public int baseAttackPower ;         //(공격력)
    //public float baseCriticalDamage ; //(크리티컬 데미지)
    public float baseCriticalChance;   //(크리티컬 확률)
    public int baseGoldGain;         //(골드 획득량)
    public float baseAutoSpeed;        // 공격속도
    public int baseHp;             //(현재 체력량)
    public int baseMp;             //(현재 마나량)


    //이 두 값은 변하는 값(런타임 상태)이기 때문에 제외
    //[Header("재화 및 상태")]
    //[HideInInspector] public int curGold = 0;     //(현재 가지고 있는 재화)
    //[HideInInspector] public int curStage = 1;    //(현재 스테이지)

    //  이 ScriptableObject는 기본값만 저장하므로, 아래는 삭제
    // 업그레이드 상태나 계산은 PlayerData나 Manager에서 따로 처리해야 함
    //[Header("업그레이드")]
    //[HideInInspector] public int attackPowerUpLevel = 0;    //(공격력 업그레이드 수치)
    //[HideInInspector] public float critiDamageUpLevel = 0f; //(크리티컬 데미지 업그레이드 수치)
    //[HideInInspector] public float critiChanceUpLevel = 0f; //(크리티컬 확률 업그레이드 수치)
    //[HideInInspector] public float goldGainUpLevel = 0f;    //(골드 획득량 업그레이드 수치)
    //[HideInInspector] public float autoSpeedUpLevel = 0f;   //(공격속도 업그레이드 수치)
    //[HideInInspector] public int hpUpLevel = 0;             //(체력량 업그레이드 수치)
    //[HideInInspector] public int mpUpLevel = 0;             //(마나량 업그레이드 수치)

    //private로 할거면 Getter필요
    // 계산 프로퍼티도 제거 (계산은 외부에서 해야 함)
    //public int UpStatusAttackPower { get { return attackPower + (attackPowerUpLevel * 5); } }
    //public float UpStatusCriticalDamage { get { return criticalDamage + (critiDamageUpLevel * 0.5f); } }
    //public float UpStatuscriticalChance { get { return criticalChance + (critiChanceUpLevel * 0.2f); } }
    //public float UpStatusGold { get { return goldGain + (goldGainUpLevel * 0.5f); } }
    //public float UpstatusAutoSpeed { get { return autoSpeed + (autoSpeedUpLevel * 0.5f); } }
    //public float UpstatusHp { get { return curHp + (hpUpLevel * 100); } }
    //public float UpstatusMp { get { return curMp + (mpUpLevel * 100); } }

    
    
}