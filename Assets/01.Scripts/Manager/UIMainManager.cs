using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainManager : MonoBehaviour
{
    public static UIMainManager Instance { get; private set; }


    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI aliveEnemysText;

    void Awake()
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

    public void UpdateStageTitle(int stageIndex)
    {
        if (stageText != null)
            stageText.text = $"Stage {stageIndex + 1}";
    }

    public void UpdateWaveTitle(int waveIndex)
    {
        if (waveText != null)
            waveText.text = $"Wave {waveIndex + 1}";
    }

    public void UpdateAliveEnemys(int aliveCount, int totalCount)
    {
        if (aliveEnemysText != null)
            aliveEnemysText.text = $"Alive Enemys: {aliveCount}/{totalCount}";
    }
}
