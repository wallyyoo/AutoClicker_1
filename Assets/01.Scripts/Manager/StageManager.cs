using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStageIndex;
    public int currentWaveIndex;
    public StageData stageData;

    public event Action OnStageCleared;
    public event Action<int> OnWaveStarted;

    public static StageManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 필요에 따라
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StartStage(int stageIndex)
    {
        currentStageIndex = stageIndex;
        currentWaveIndex = 0;
        StartWave(currentWaveIndex);
    }

    public void StartWave(int waveIndex)
    {
        var wave = stageData.stages[currentStageIndex].waves[waveIndex];
        EnemyManager.Instance.SpawnWave(wave.enemys);
        OnWaveStarted?.Invoke(waveIndex);
    }

    public void OnWaveCleared()
    {
        currentWaveIndex++;
        if (currentWaveIndex < stageData.stages[currentStageIndex].waves.Count)
        {
            StartWave(currentWaveIndex);
        }
        else
        {
            GiveReward();
            OnStageCleared?.Invoke();// 스테이지 클리어 이벤트 발생
            // 다음 스테이지로 이동 등
        }
    }

    private void GiveReward()
    {
        // 리워드 지급 로직
    }
}