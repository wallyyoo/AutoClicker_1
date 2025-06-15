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
    public TextMeshProUGUI criticalRateText; //  UI 텍스트 연결

    [Header("자동공격 업그레이드")]
    public AutoAttack autoAttack;
    public Button attackSpeedUpgradeButton;
    public TextMeshProUGUI attackIntervalText; //  UI 텍스트 연결

    [Header("골드 획득량 업그레이드")]
    public Button goldGainUpgradeButton;
    public TextMeshProUGUI goldGainText;

    public static float criticalRate = 0.3f;
    public static int goldGain = 0;
    public float holdInterval = 0.2f;//버튼 꾹 누르고있으면 0.2초마다 업그레이드

    private Coroutine critHoldRoutine;
    private Coroutine speedHoldRoutine;
    private Coroutine goldHoldRoutine;

    // Start is called before the first frame update
    void Start()
    {
        criticalRateUpgradeButton.onClick.AddListener(OnClickCriticalRateUpgrade);
        attackSpeedUpgradeButton.onClick.AddListener(OnClickAttackSpeedUpgrade);
        goldGainUpgradeButton.onClick.AddListener(OnClickGoldGainUpgrade);

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

        UpdateCriticalRateText(); // 초기값 표시
        UpdateAttackIntervalText(); // 초기 쿨타임 텍스트 출력
        UpdateGoldGainText();
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

    //void OnClickCriticalRateUpgrade()
    //{
    //    if (criticalRate >= 1f)
    //    {
    //        Debug.Log("이미 최대 치명타 확률입니다!");
    //        return;
    //    }

    //    UpgradeCriticalRate(0.1f);
    //    UpdateCriticalRateText();
    //}
    void OnClickCriticalRateUpgrade()
    {
        var data = GameManager.Instance.playerData;
        var rule = GameManager.Instance.playerUpgradeTable;

        if (data.critiChanceUpLevel >= 18)
        {
            Debug.Log("최대 치명타 업그레이드 레벨!");
            return;
        }

        data.critiChanceUpLevel++;
        GameManager.Instance.JsonSave();
        UpdateCriticalRateText();
    }
    //void UpgradeCriticalRate(float amount)
    //{
    //    criticalRate += amount;
    //    criticalRate = Mathf.Min(criticalRate, 1f); // 100% 제한
    //    Debug.Log("치명타 확률 증가!");
    //}
    void UpdateCriticalRateText()
    {
        var baseVal = GameManager.Instance.playerStatData.baseCriticalChance;
        var upVal = GameManager.Instance.playerUpgradeTable.critChancePerLevel;
        var level = GameManager.Instance.playerData.critiChanceUpLevel;

        float final = Mathf.Min(baseVal + level * upVal, 1f);
        criticalRateText.text = $"치명타 {Mathf.RoundToInt(final * 100)}%";
    }

    //void UpdateCriticalRateText()
    //{
    //    int percent = Mathf.RoundToInt(criticalRate * 100f);
    //    criticalRateText.text = $"치명타 {percent}%";
    //}




    //void OnClickAttackSpeedUpgrade()
    //{
    //    if (autoAttack.attackInterval <= 0.1f)
    //    {
    //        Debug.Log("최소 쿨타임 도달!");
    //        attackSpeedUpgradeButton.interactable = false;
    //        return;
    //    }

    //    UpgradeAttackInterval(0.1f);
    //    UpdateAttackIntervalText();
    //}
    void OnClickAttackSpeedUpgrade()
    {
        var data = GameManager.Instance.playerData;
        var rule = GameManager.Instance.playerUpgradeTable;

        float current = GameManager.Instance.playerStatData.baseAutoSpeed - data.autoSpeedUpLevel * rule.autoSpeedPerLevel;

        if (current <= 0.1f)
        {
            Debug.Log("최소 쿨타임 도달!");
            attackSpeedUpgradeButton.interactable = false;
            return;
        }

        data.autoSpeedUpLevel++;
        GameManager.Instance.JsonSave();
        UpdateAttackIntervalText();
    }
    //void UpgradeAttackInterval(float amount)
    //{
    //    autoAttack.attackInterval = Mathf.Max(0.1f, autoAttack.attackInterval - amount);
    //    Debug.Log("자동 공격 속도 상승!");
    //}
    //void UpdateAttackIntervalText()
    //{
    //    float interval = autoAttack.attackInterval;
    //    attackIntervalText.text = $"자동공격 {interval:0.0}초";
    //}
    void UpdateAttackIntervalText()
    {
        var baseVal = GameManager.Instance.playerStatData.baseAutoSpeed;
        var upVal = GameManager.Instance.playerUpgradeTable.autoSpeedPerLevel;
        var level = GameManager.Instance.playerData.autoSpeedUpLevel;

        float interval = Mathf.Max(0.1f, baseVal - level * upVal);
        autoAttack.attackInterval = interval;
        attackIntervalText.text = $"자동공격 {interval:0.0}초";
    }
    //void OnClickGoldGainUpgrade()
    //{

    //    if (goldGain >= 100)
    //    {
    //        Debug.Log("이미 최대 골드 획득량입니다!");
    //        return;
    //    }
    //    UpgradeGoldGain(1);// 골드 획득량 증가수치
    //    UpdateGoldGainText();
    //}
    void OnClickGoldGainUpgrade()
    {
        var data = GameManager.Instance.playerData;

        if (data.goldGainUpLevel >= 100)
        {
            Debug.Log("골드 획득량 최대!");
            return;
        }

        data.goldGainUpLevel++;
        GameManager.Instance.JsonSave();
        UpdateGoldGainText();
    }
    //void UpgradeGoldGain(int amount)
    //{
    //    goldGain += amount;
    //    goldGain = Mathf.Min(goldGain, 100); // 100원까지 제한 (원하면 제거 가능)
    //    Debug.Log("골드 획득량 증가!");
    //}
    //void UpdateGoldGainText()
    //{
    //    goldGainText.text = $"골드획득 {goldGain}원";
    //}
    void UpdateGoldGainText()
    {
        var baseVal = GameManager.Instance.playerStatData.baseGoldGain;
        var upVal = GameManager.Instance.playerUpgradeTable.goldGainPerLevel;
        var level = GameManager.Instance.playerData.goldGainUpLevel;

        int total = baseVal + level * upVal;
        goldGainText.text = $"골드획득 {total}원";
    }
}
