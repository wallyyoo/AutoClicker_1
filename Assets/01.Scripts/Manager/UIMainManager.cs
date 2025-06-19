
using UnityEngine;
using TMPro;
/// <summary>
/// 메인 UI의 텍스트 정보 업데이트 및 클릭 사운드 재생 담당
/// </summary>
public class UIMainManager : MonoBehaviour
{
    public static UIMainManager Instance { get; private set; }

    public OptionButton optionButton;

    [Header("UI Elements")]

    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI aliveEnemysText;
    [SerializeField] private TextMeshProUGUI curGoldText;
    
    [Header("사운드 효과")]
    public SoundData clickSound;

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

    public void RefreshCurGold()
    {
        curGoldText.text = GameManager.Instance.playerData.curGold.ToString();
    }

    public void OnClickSound()
    {
        TestSoundManager.TestSoundInstance.PlaySFX(clickSound);
    }
}
