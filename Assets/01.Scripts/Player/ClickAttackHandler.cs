using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAttackHandler : MonoBehaviour
{
    public AttackEffect attackEffect;
    public Animator animator;
    private void OnDisable()
    {
        ClickManager.OnClick -= HandleClick;
    }

    private void OnEnable()
    {
        ClickManager.OnClick += HandleClick;
    }
    void HandleClick(Vector3 clickPosition)
    {
        // 플레이어 데이터 가져오기
        var playerData = GameManager.Instance.playerData;
        // 치명타 판정 및 데미지 계산
        bool isCritical = AttackUtility.IsCritical(playerData.UpStatuscriticalChance);

        attackEffect?.PlayHitEffect(clickPosition, isCritical);
        
        Enemy target = FindEnemyAtPosition(clickPosition);
        if (target != null)
        {
            int damage = AttackUtility.CalculateDamage(
                playerData.UpStatusAttackPower,
                playerData.UpStatusCriticalDamage,
                isCritical
            );
            target.TakeDamage(damage);
            animator?.SetTrigger("Attack");

            //Debug.Log($"[ClickAttack] {target.name}에게 {damage} 데미지 (치명타: {isCritical})");
        }
        else
        {
            //Debug.Log($"[ClickAttack] 적 없음 (이펙트만 출력됨)");
        }
    }
    Enemy FindEnemyAtPosition(Vector3 position)
    {
        // 클릭 위치에 정확히 몬스터가 있는지 확인
        LayerMask enemyMask = LayerMask.GetMask("Enemy");
        Collider2D hit = Physics2D.OverlapPoint(position, enemyMask);
        return hit?.GetComponent<Enemy>();
    }
}
