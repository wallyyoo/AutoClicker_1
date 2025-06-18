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
        var data = GameManager.Instance.playerData;

        if (data.UpStatuscriticalChance >= 1)
        {
            Debug.Log("최대 치명타확률 업그레이드!");
            return;
        }

        data.critiChanceUpLevel++;
        Debug.Log(data.critiChanceUpLevel);
        Json.JsonSave();
        UIStatText.UpdateCriticalRateText();

    }
   
 


    void OnClickAttackSpeedUpgrade()
    {
        var data = GameManager.Instance.playerData;

        if (data.UpstatusAutoSpeed <= 0.1f)
        {
            return;
        }
        data.autoSpeedUpLevel++;
        Json.JsonSave();
        UIStatText.UpdateAttackSpeedText();
        //  자동공격 재시작 (갱신된 공격 간격 반영)
        autoAttack.RestartAutoAttack();
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
        Json.JsonSave();
        UIStatText.UpdateGoldGainText();
    }

}
