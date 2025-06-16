using TMPro;
using UnityEngine;

public class GoldDelta : MonoBehaviour
{
    // 몬스터 잡을 때 획득하는 골드(골드 획득량 래밸에 따른 골드 획득량 증가)
    public void AddGold()
    {
        int gold = (int)GameManager.Instance.playerData.UpStatusGold ;// 여기에 몬스터 잡은 후 얻게되는 골드를 곱한다.

        GameManager.Instance.playerData.curGold += gold;

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
