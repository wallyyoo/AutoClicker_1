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
        // 선형 증가: 10%씩 증가
        float healthMultiplier = 1f + stageIndex * 0.1f;
        float damageMultiplier = 1f + stageIndex * 0.1f;

        currentHealth = Mathf.RoundToInt(data.health * healthMultiplier);
        currentDamage = Mathf.RoundToInt(data.damage * damageMultiplier);
    }
}
