using System.Collections.Generic;
using UnityEngine;

// �������� ������ ScriptableObject�� ����
[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 1)]
public class StageData : ScriptableObject
{
    public List<StageInfo> stages; // ���� �������� ������ ����Ʈ�� ����
}

[System.Serializable]
public class StageInfo
{
    public int stageKey;
    public List<WaveData> waves; // �迭 ��� ����Ʈ�� ���� (�ν����Ϳ��� ��)
}

[System.Serializable]
public class WaveData
{
    public List<MonsterSpawnData> enemys; // �迭 ��� ����Ʈ�� ����
}

[System.Serializable]
public class MonsterSpawnData
{
    public EnemyData.EnemyType enemyType;
    public int spawnCount;
}