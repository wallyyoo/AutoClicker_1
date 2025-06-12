using UnityEngine;
using System.Collections;

public class AutoAttack : MonoBehaviour
{
    public float attackInterval = 1.5f;//공격 간격
    public bool isAutoAttackEnabled = true;//자동 공격 활성화 여부
    public AttackEffect attackEffect; // <- 연결 필요!
    void Start()
    {
        StartCoroutine(AutoAttackRoutine());
    }

    IEnumerator AutoAttackRoutine()
    {
        while (isAutoAttackEnabled)
        {
            Attack(); // 나중에 연결
            yield return new WaitForSeconds(attackInterval);//attackInterval(초)만큼 잠시 기다림
        }
    }

    void Attack()
    {
        Vector3 attackPos = transform.position + Vector3.up * 1f; // 예시 위치
        attackEffect.HandleClick(attackPos); // <- 여기서 크리티컬 판정 + 파티클 출력
        Debug.Log($"자동 공격 속도!{attackInterval}"); 
    }
    public void UpgradeAttackSpeed(float amount)
    {
        attackInterval = Mathf.Max(0.1f, attackInterval - amount); // 최소 쿨타임 제한
    }
}