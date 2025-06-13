using UnityEngine;
using System.Collections;

public class AutoAttack : MonoBehaviour
{
    public float attackInterval = 1.5f;//공격 간격
    public bool isAutoAttackEnabled = true;//자동 공격 활성화 여부
    public AttackEffect attackEffect; // <- 연결 필요!

    Coroutine attackRoutine;

    void Start()
    {
        //StartCoroutine(AutoAttackRoutine());//업그레이드 후 attackInterval이 바로 반영이 안될수 있으므로
        attackRoutine = StartCoroutine(AutoAttackRoutine());
        
    }

    IEnumerator AutoAttackRoutine()
    {
        while (isAutoAttackEnabled)
        {
            Attack();
            yield return new WaitForSeconds(attackInterval);//attackInterval(초)만큼 잠시 기다림
        }
    }
    public void RestartAutoAttack()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        attackRoutine = StartCoroutine(AutoAttackRoutine()); // 최신 쿨타임 반영
    }
    void Attack()
    {
        Vector3 attackPos = transform.position + Vector3.up * 1f; // 예시 위치
        attackEffect.HandleClick(attackPos); // <- 여기서 크리티컬 판정 + 파티클 출력
        Debug.Log($"자동 공격 속도!{attackInterval}"); 
    }
   
}