using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    public void SpawnWave(List<MonsterSpawnData> enemys)
    {
        // enemys 리스트를 순회하며 에너미 스폰
        // 스폰된 에너미를 aliveEnemies에 추가
    }

    public void OnEnemyDied(Enemy enemy)
    {
        aliveEnemies.Remove(enemy);// 에너미 제거
        if (aliveEnemies.Count == 0)
        {
            // 모든 에너미가 제거됨
            StageManager.Instance.OnWaveCleared();
        }
    }
}
