using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int currentStageIndex = 0;
    public int currentWaveIndex;
    [SerializeField] public StageData stageData;

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
        if (Json.saveData == null)
        {
            Json.JsonSave();
        }
        // 게임 시작 시 0스테이지 자동 시작
        GameManager.Instance.soundManager.Bgm(Json.saveData.curStage);
        
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
            GiveReward();
            OnStageCleared?.Invoke();

            // 다음 스테이지 자동 진행
            StartStage(currentStageIndex + 1);
            GameManager.Instance.soundManager.Bgm(currentStageIndex + 1);

        }
    }

    private void GiveReward()
    {
        // 리워드 지급 로직
    }
}