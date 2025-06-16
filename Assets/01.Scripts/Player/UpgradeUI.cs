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

    public float holdInterval = 0.2f;//버튼 꾹 누르고있으면 0.2초마다 업그레이드

    private Coroutine critHoldRoutine;
    private Coroutine speedHoldRoutine;
    private Coroutine goldHoldRoutine;

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

  
    void OnClickCriticalRateUpgrade()
    {
        var data = GameManager.Instance.playerData;

        if (data.critiChanceUpLevel >= 18)
        {
            Debug.Log("최대 치명타 업그레이드 레벨!");
            return;
        }

        data.critiChanceUpLevel++;
        GameManager.Instance.JsonSave();
        UpdateCriticalRateText();
    }
  
    void UpdateCriticalRateText()
    {

        float final = Mathf.Min( GameManager.Instance.playerData.UpStatuscriticalChance,1f);

        criticalRateText.text = $"치명타 {Mathf.RoundToInt(final * 100)}%";
    }
 

    void OnClickAttackSpeedUpgrade()
    {
        var data = GameManager.Instance.playerData;

        if (data.autoSpeedUpLevel >= 20f)
        {
            return;
        }
        data.autoSpeedUpLevel++;
        GameManager.Instance.JsonSave();
        UpdateAttackIntervalText();
    }
 
    void UpdateAttackIntervalText()
    {
        float interval = GameManager.Instance.playerData.UpstatusAutoSpeed;
        autoAttack.attackInterval = interval;
        attackIntervalText.text = $"자동공격 {interval:0.0}초";
    }
  
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

    void UpdateGoldGainText()
    {
        float total = GameManager.Instance.playerData.UpStatusGold;
        goldGainText.text = $"골드획득 + {total:0.0}원";
    }
}
