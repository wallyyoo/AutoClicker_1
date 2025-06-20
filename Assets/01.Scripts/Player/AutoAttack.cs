﻿using UnityEngine;
using System.Collections;

public class AutoAttack : MonoBehaviour
{
    public float attackInterval;//공격 간격
    public bool isAutoAttackEnabled = true;//자동 공격 활성화 여부
    public AttackEffect attackEffect; // <- 연결 필요!
    

    Coroutine attackRoutine;

    void Start()
    {
        // 업그레이드 수치 기반으로 공격 간격 계산
        float interval = GameManager.Instance.playerData.UpstatusAutoSpeed;
        attackInterval = interval;

        StartAutoAttack();

    }
    public void StartAutoAttack()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        attackRoutine = StartCoroutine(AutoAttackRoutine());
    }
    IEnumerator AutoAttackRoutine()
    {
        while (isAutoAttackEnabled)
        {
            //Debug.Log($"[AutoAttack] 공격 간격: {attackInterval}");
            autoAttack();
            yield return new WaitForSeconds(attackInterval);//attackInterval(초)만큼 잠시 기다림
        }
    }
    public void RestartAutoAttack()
    {
        StartAutoAttack();
    }
  
    void autoAttack()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없음");
            return;
        }

        var detector = player.GetComponentInChildren<EnemyDetector>();
        if (detector == null)
        {
            Debug.LogWarning("EnemyDetector 없음");
            return;
        }

        Vector3 attackPos = transform.position + Vector3.right * 3f;
        var playerData = GameManager.Instance.playerData;

        bool isCritical = AttackUtility.IsCritical(playerData.UpStatuscriticalChance);
        int damage = AttackUtility.CalculateDamage(playerData.UpStatusAttackPower, playerData.UpStatusCriticalDamage, isCritical);

        Enemy target = detector.GetNearestEnemy(attackPos);
        Vector3 effectPos = target != null ? target.transform.position : attackPos;

        if (target != null)
        {
            Debug.Log($"공격! 대상: {target.name}, 데미지: {damage}, 크리티컬: {isCritical}");
            target.TakeDamage(damage);
        }
        else
        {
            //Debug.Log("공격했지만 대상 없음");
        }
        attackEffect?.PlayHitEffect(effectPos, isCritical);

    }
    public void RecalculateAttackInterval()//자동공격속도 업그레이드시 호출하여 속도 갱신
    {
        var baseVal = GameManager.Instance.playerData.baseAutoSpeed;
        var perLevel = GameManager.Instance.playerData.playerUpgradeTable.autoSpeedPerLevel;
        var level = GameManager.Instance.playerData.autoSpeedUpLevel;

        attackInterval = Mathf.Max(0.1f, baseVal - level * perLevel);
    }
}
