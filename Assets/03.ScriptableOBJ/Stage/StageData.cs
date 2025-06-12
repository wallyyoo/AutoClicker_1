using System.Collections.Generic;
using UnityEngine;

// 스테이지 정보를 ScriptableObject로 관리
[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData", order = 1)]
public class StageData : ScriptableObject
{
    public List<StageInfo> stages; // 여러 스테이지 정보를 리스트로 관리
}

[System.Serializable]
public class StageInfo
{
    public int stageKey;
    public List<WaveData> waves; // 배열 대신 리스트로 변경 (인스펙터에서 편리)
}

[System.Serializable]
public class WaveData
{
    public List<MonsterSpawnData> enemys; // 배열 대신 리스트로 변경
}

[System.Serializable]
public class MonsterSpawnData
{
    public EnemyData.EnemyType enemyType;
    public int spawnCount;
}