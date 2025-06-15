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
    public int stageKey;// 스테이지 키 값 (예: 1, 2, 3 등)
    public List<WaveData> waves; // 각 스테이지에 여러 웨이브 정보를 리스트로 관리
}

[System.Serializable]
public class WaveData
{
    public List<MonsterSpawnData> enemys; // 각 웨이브에 등장하는 몬스터 정보 리스트
}

[System.Serializable]
public class MonsterSpawnData
{
    public GameObject enemyPrefab; // 인스펙터에서 직접 프리팹 할당
    public Vector2 spawnPosition;       // 실제 소환 위치(스폰 포인트)
    public Vector2 arrivalPosition; // 스폰된 후 몬스터가 도착할 위치
}