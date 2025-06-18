using TMPro;
using UnityEngine;

public class GoldDelta : MonoBehaviour
{
    // 몬스터 잡을 때 획득하는 골드(골드 획득량 래밸에 따른 골드 획득량 증가)
    public static void AddGold(int amount)
    {

        GameManager.Instance.playerData.curGold += amount;

        Json.JsonSave();
    }

    // 업그레이드나 아이템 구매 시 골드 차감
    public void SubGold(GameObject gameObject)
    {
       
        TextMeshProUGUI uiText = gameObject.GetComponent<TextMeshProUGUI>();
        int.TryParse(uiText.text, out int value);

        if (GameManager.Instance.playerData.curGold < value)
        {
            // 골드가 부족합니다 팝업창 생성
        }
        
        GameManager.Instance.playerData.curGold -= value;

        Json.JsonSave();
    }
}
