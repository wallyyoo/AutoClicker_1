using System;
using UnityEngine;

public class StageManager : MonoBehaviour, IRewardable
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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 게임 시작 시 0스테이지 자동 시작
        GameManager.Instance.soundManager.Bgm(currentStageIndex);
        StartStage(0);
    }

    public void StartStage(int stageIndex)
    {
        if (stageIndex >= stageData.stages.Count)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

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
            int totalReward = 100 + 10 * (currentStageIndex + 1); // 스테이지 클리어 보상 계산 (예: 100 + 10 * 스테이지 번호)
            AddGold(totalReward);
            OnStageCleared?.Invoke();

            // 다음 스테이지 자동 진행
            StartStage(currentStageIndex + 1);
            GameManager.Instance.soundManager.Bgm(currentStageIndex + 1);

        }
    }

    public void AddGold(int amount)
    {
        GameManager.Instance.playerData.curGold += amount;
        Debug.Log($"골드 획득: {amount}. 현재 골드: {GameManager.Instance.playerData.curGold}");

        Json.JsonSave();
    }
}