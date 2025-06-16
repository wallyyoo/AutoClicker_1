using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void SpawnWave(List<MonsterSpawnData> enemys)
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            var spawnData = enemys[i];
            // �� ���ʹ��� spawnPosition���� ��ȯ
            GameObject go = Instantiate(spawnData.enemyPrefab, spawnData.spawnPosition, Quaternion.identity);
            Enemy enemy = go.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.stageData = StageManager.Instance.stageData;
                enemy.Init(spawnData.enemyPrefab.GetComponent<Enemy>().data, StageManager.Instance.currentStageIndex);
                enemy.SetArrivalPosition(spawnData.arrivalPosition);
                aliveEnemies.Add(enemy);
            }
        }
    }

    public void OnEnemyDied(Enemy enemy)
    {
        aliveEnemies.Remove(enemy);// ���ʹ� ����
        if (aliveEnemies.Count == 0)
        {
            // ��� ���ʹ̰� ���ŵ�
            StageManager.Instance.OnWaveCleared();
        }
    }
}
