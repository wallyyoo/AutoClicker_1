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
            DontDestroyOnLoad(gameObject);
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
        var wave = stageData.stages[currentStageIndex].waves[waveIndex];// ���� ���������� �ش� ���̺� ���� ��������
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
            OnStageCleared?.Invoke();// �������� Ŭ���� �̺�Ʈ �߻�
            // ���� ���������� �̵� ��
        }
    }

    private void GiveReward()
    {
        // ������ ���� ����
    }
}