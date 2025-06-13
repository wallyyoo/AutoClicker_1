using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    private int currentHealth;
    private int currentDamage;

    public void Init(EnemyData enemyData, int stageIndex)
    {
        data = enemyData;
        // 선형 증가: 10%씩 증가로 임시 구현
        float healthMultiplier = 1f + stageIndex * 0.1f;
        float damageMultiplier = 1f + stageIndex * 0.1f;

        currentHealth = Mathf.RoundToInt(data.health * healthMultiplier);
        currentDamage = Mathf.RoundToInt(data.damage * damageMultiplier);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //피격 애니메이션 구현 예정입니다.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 사망 처리 (이펙트, 점수, EnemyManager에 알리기,사망 애니메이션 등을 구현 예정입니다)

        Destroy(gameObject);
    }
}
