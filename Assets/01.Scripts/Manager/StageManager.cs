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
        Json.JsonLoad();
        GameManager.Instance.soundManager.Bgm(currentStageIndex);
        if (currentStageIndex < 0)
        {
            currentStageIndex = 0;
            StartStage(0);
            return;
        }
        StartStage(currentStageIndex);
    }

    public void StartStage(int stageIndex)
    {
        if (stageIndex >= stageData.stages.Count)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

        currentStageIndex = stageIndex;
        UIMainManager.Instance.UpdateStageTitle(currentStageIndex);
        StartWave(currentWaveIndex);
    }

    public void StartWave(int waveIndex)
    {
        var wave = stageData.stages[currentStageIndex].waves[waveIndex];
        UIMainManager.Instance.UpdateWaveTitle(waveIndex);
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
            AddGold(currentStageIndex);
            OnStageCleared?.Invoke();

            // 다음 스테이지 자동 진행
            StartStage(currentStageIndex + 1);
            GameManager.Instance.soundManager.Bgm(currentStageIndex + 1);

        }
        Json.JsonSave();
    }

    public void AddGold(int amount)
    {

        int totalReward = 100 + 10 * (amount + 1); // 스테이지 클리어 보상 계산
        GameManager.Instance.playerData.curGold += totalReward;
        Debug.Log($"골드 획득: {amount}. 현재 골드: {GameManager.Instance.playerData.curGold}");

        Json.JsonSave();
    }
}