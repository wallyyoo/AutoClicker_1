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
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 attackPos = transform.position + Vector3.right * 3f;



        //Vector2 boxSize = new Vector2(1.0f, 1.0f);
        // 플레이어 콜라이더 가져오기
        BoxCollider2D boxCollider = player.GetComponentInChildren<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogWarning("플레이어에 BoxCollider2D가 없습니다.");
            return;
        }

        // 콜라이더 크기와 위치 반영
        Vector2 boxSize = new Vector2(
            boxCollider.size.x * boxCollider.transform.lossyScale.x,
            boxCollider.size.y * boxCollider.transform.lossyScale.y
        );
        Vector2 boxCenter = (Vector2)boxCollider.transform.position + boxCollider.offset;

        //치명타 확률 및 데미지 계산
        bool isCritical = Random.value < GetCurrentCriticalRate();

        int damage = GameManager.Instance.playerData.UpStatusAttackPower;

        if (isCritical)
        {
            float critMultiplier = GameManager.Instance.playerData.UpStatusCriticalDamage;
            damage = Mathf.RoundToInt(damage * (critMultiplier));
        }

        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPos, boxSize, 0f, LayerMask.GetMask("Enemy"));

        Debug.Log($"적 {hits.Length}명 감지됨");


        if (attackEffect != null)
        {
            attackEffect.PlayHitEffect(attackPos, isCritical);
        }
        else
        {
            Debug.LogWarning("AttackEffect가 연결 안됨!");
        }

        float minDistance = float.MaxValue;
        Enemy targetEnemy = null;

        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null && enemy.isArrived)//움직이지 않는 적 공격
            {
                // 가장 가까운 적 찾기
                float distance = Vector2.Distance(attackPos, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetEnemy = enemy;
                }
            }
        }
        if (targetEnemy != null)
        {
            Debug.Log("가장 가까운 도착한 적에게 데미지 전달됨!");
            targetEnemy.TakeDamage(damage);

            if (attackEffect != null)
            {
                attackEffect.PlayHitEffect(targetEnemy.transform.position, isCritical);
            }
        }
        else
        {
            Debug.Log("범위 내 도착한 적이 없습니다.");
        }
    }



    //Debug.Log($"[AutoAttack] 공격 간격: {attackInterval:0.00}초");
    //Debug.Log(isCritical ? $"치명타! 데미지 {damage}" : $"일반 공격 데미지 {damage}");
    float GetCurrentCriticalRate()
    {
        return Mathf.Min(GameManager.Instance.playerData.UpStatuscriticalChance, 1f); // 최대 100%
    }
}
