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
    public int stageKey;// �������� Ű �� (��: 1, 2, 3 ��)
    public List<WaveData> waves; // �� ���������� ���� ���̺� ������ ����Ʈ�� ����
}

[System.Serializable]
public class WaveData
{
    public List<MonsterSpawnData> enemys; // �� ���̺꿡 �����ϴ� ���� ���� ����Ʈ
}

[System.Serializable]
public class MonsterSpawnData
{
    public GameObject enemyPrefab; // �ν����Ϳ��� ���� ������ �Ҵ�
    public Vector3 arrivalPosition; // ������ �� ���Ͱ� ������ ��ġ
}