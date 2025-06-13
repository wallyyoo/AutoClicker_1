using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    public void SpawnWave(List<MonsterSpawnData> enemys)
    {
        // enemys ����Ʈ�� ��ȸ�ϸ� ���ʹ� ����
        // ������ ���ʹ̸� aliveEnemies�� �߰�
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
