using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UpgradeUI : MonoBehaviour
{
    [Header("크리티컬 업그레이드")]
    public Button criticalRateUpgradeButton;
    [Header("자동공격 업그레이드")]
    public AutoAttack autoAttack;
    public Button attackSpeedUpgradeButton;
    [Header("골드 획득량 업그레이드")]
    public Button goldGainUpgradeButton;
    public float holdInterval = 0.2f;//버튼 꾹 누르고있으면 0.2초마다 업그레이드
    [Header("업그레이드 골드 비용")]
    public TextMeshProUGUI criticalUpText;
    public TextMeshProUGUI autoUpGoldText;
    public TextMeshProUGUI goldGainUpText;
    private Coroutine critHoldRoutine;
    private Coroutine speedHoldRoutine;
    private Coroutine goldHoldRoutine;
    public UIStatText UIStatText; // UI 스탯 텍스트 연결
    // Start is called before the first frame update
    void Start()
    {
        //criticalRateUpgradeButton.onClick.AddListener(OnClickCriticalRateUpgrade);
        //attackSpeedUpgradeButton.onClick.AddListener(OnClickAttackSpeedUpgrade);
        //goldGainUpgradeButton.onClick.AddListener(OnClickGoldGainUpgrade);
        // 꾹 누름 감지용 EventTrigger 추가
        AddHoldEvent(criticalRateUpgradeButton,
            () => critHoldRoutine = StartCoroutine(HoldUpgradeRoutine(OnClickCriticalRateUpgrade)),
            () => StopRoutine(ref critHoldRoutine));
        AddHoldEvent(attackSpeedUpgradeButton,
            () => speedHoldRoutine = StartCoroutine(HoldUpgradeRoutine(OnClickAttackSpeedUpgrade)),
            () => StopRoutine(ref speedHoldRoutine));
        AddHoldEvent(goldGainUpgradeButton,
            () => goldHoldRoutine = StartCoroutine(HoldUpgradeRoutine(OnClickGoldGainUpgrade)),
            () => StopRoutine(ref goldHoldRoutine));
          StartCoroutine(InitUITexts());
    }
    private IEnumerator InitUITexts()
    {
        yield return null; // 1프레임 대기 (GameManager.Awake() 보장)
        if (GameManager.Instance?.playerData == null || GameManager.Instance.playerData.playerUpgradeTable == null)
        {
            Debug.LogError("[UpgradeUI] playerData 또는 playerUpgradeTable이 초기화되지 않았습니다.");
            yield break;
        }
        UIStatText.UpdateCriticalRateText();
        UIStatText.UpdateAttackSpeedText();
        UIStatText.UpdateGoldGainText();
    }
    void AddHoldEvent(Button button, System.Action onDown, System.Action onUp)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = button.gameObject.AddComponent<EventTrigger>();
        // PointerDown
        var entryDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entryDown.callback.AddListener((_) => onDown());
        trigger.triggers.Add(entryDown);
        // PointerUp
        var entryUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        entryUp.callback.AddListener((_) => onUp());
        trigger.triggers.Add(entryUp);
    }
    IEnumerator HoldUpgradeRoutine(System.Action upgradeAction)
    {
        while (true)
        {
            upgradeAction();
            yield return new WaitForSeconds(holdInterval);
        }
    }
    void StopRoutine(ref Coroutine routine)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
    }
    void OnClickCriticalRateUpgrade()
    {
        var playerData = GameManager.Instance.playerData;
        if (playerData.UpStatuscriticalChance >= 1f)
        {
            Debug.Log("치명타 확률이 최대치입니다!");
            return;
        }
        int cost = (int)playerData.UpCriticalGold;
        if (playerData.curGold < cost)
        {
            Debug.Log("골드가 부족합니다!");
            return;
        }
        playerData.curGold -= cost;
        playerData.critiChanceUpLevel++;
        Json.JsonSave();
        UIStatText.UpdateCriticalRateText();
        UIMainManager.Instance.RefreshCurGold();
    }
    void OnClickAttackSpeedUpgrade()
    {
        var playerData = GameManager.Instance.playerData;
        if (playerData.UpstatusAutoSpeed <= 0.1f)
        {
            Debug.Log("공격속도 최소값 도달");
            return;
        }
        int cost = (int)playerData.UpAutoSpeedGold;
        if (playerData.curGold < cost)
        {
            Debug.Log("골드가 부족합니다!");
            return;
        }
        playerData.curGold -= cost;
        playerData.autoSpeedUpLevel++;
        Json.JsonSave();
        // UI 업데이트
        UIStatText.UpdateAttackSpeedText();
        UIMainManager.Instance.RefreshCurGold();
        // 자동공격 갱신
        autoAttack.RecalculateAttackInterval();
        autoAttack.RestartAutoAttack();
    }
    void OnClickGoldGainUpgrade()
    {
        var playerData = GameManager.Instance.playerData;
        if (playerData.goldGainUpLevel >= 100)
        {
            Debug.Log("골드 획득량 업그레이드 최대입니다!");
            return;
        }
        int cost = (int)playerData.UpGoldGainGold;
        if (playerData.curGold < cost)
        {
            Debug.Log("골드가 부족합니다!");
            return;
        }
        playerData.curGold -= cost;
        playerData.goldGainUpLevel++;
        Json.JsonSave();
        UIStatText.UpdateGoldGainText();
        UIMainManager.Instance.RefreshCurGold();
    }
}