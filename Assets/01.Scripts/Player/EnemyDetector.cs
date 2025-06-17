using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private bool monsterCheking = false;

    public List<Enemy> detectedEnemies = new List<Enemy>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && !detectedEnemies.Contains(enemy))
        {
            detectedEnemies.Add(enemy);
            Debug.Log($"[EnemyDetector] 감지됨: {enemy.name}");

            monsterCheking = true;
            if (monsterCheking == true)
            {
                BackGroundManager.BackInstace.BackGroundAllMoveStop();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && detectedEnemies.Contains(enemy))
        {
            detectedEnemies.Remove(enemy);
            Debug.Log($"[EnemyDetector] 범위 벗어남: {enemy.name}");

            monsterCheking = false;
            if (monsterCheking == false)
            {
                BackGroundManager.BackInstace.ResetAllSpeeds();
            }
        }
    }
    public Enemy GetNearestEnemy(Vector3 fromPos)
    {
        float minDist = float.MaxValue;
        Enemy nearest = null;
        foreach (var enemy in detectedEnemies)
        {
            if (enemy == null || !enemy.isArrived) continue;
            float dist = Vector2.Distance(fromPos, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }
        return nearest;
    }
}
