using UnityEngine;
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
            Attack();
            yield return new WaitForSeconds(attackInterval);//attackInterval(초)만큼 잠시 기다림
        }
    }
  
    public void RestartAutoAttack()
    {
        StartAutoAttack();
    }
    void Attack()
    {
        Vector3 attackPos = transform.position + Vector3.right * 3f;

        //치명타 확률 및 데미지 계산
        bool isCritical = Random.value < GetCurrentCriticalRate();

        int damage = GameManager.Instance.playerData.UpStatusAttackPower;

        if (isCritical)
        {
            float critMultiplier = GameManager.Instance.playerData.UpStatusCriticalDamage;
            damage = Mathf.RoundToInt(damage * (critMultiplier));
        }

        Vector2 boxSize = new Vector2(1.0f, 1.0f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPos, boxSize, 0f, LayerMask.GetMask("Enemy"));

        Debug.Log($"적 {hits.Length}명 감지됨");

        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("적에게 데미지 전달됨!");
                Debug.Log($"데미지 : {damage}");
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Enemy 스크립트 없음");
            }
        }


        if (attackEffect != null) { 
        attackEffect.PlayHitEffect(attackPos, isCritical);
        }
        else
            Debug.LogWarning("AttackEffect가 연결 안됨!");
        //Debug.Log($"[AutoAttack] 공격 간격: {attackInterval:0.00}초");
        //Debug.Log(isCritical ? $"치명타! 데미지 {damage}" : $"일반 공격 데미지 {damage}");
    }
    float GetCurrentCriticalRate()
    {
        return Mathf.Min(GameManager.Instance.playerData.UpStatuscriticalChance, 1f); // 최대 100%
    }
}