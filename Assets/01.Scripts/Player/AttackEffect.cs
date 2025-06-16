using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;
    void OnEnable()
    {
        ClickManager.OnClick += HandleClick;
    }

    void OnDisable()
    {
        ClickManager.OnClick -= HandleClick;
    }

    public void HandleClick(Vector3 position)
    {
        float critRate = GetCurrentCriticalRate();
        bool isCritical = Random.value < critRate;
        PlayHitEffect(position, isCritical);
        //Debug.Log(isCritical ? $"치명타! 확률: {critRate * 100:F1}%" : $"일반 공격 (치명타 확률: {critRate * 100:F1}%)");
        //Debug.Log(isCritical ? $"치명타!{UpgradeUI.criticalRate}" : "일반 공격");
    }

    float GetCurrentCriticalRate()
    {
        var baseCrit = GameManager.Instance.playerStatData.baseCriticalChance;
        var perLevel = GameManager.Instance.playerUpgradeTable.critChancePerLevel;
        var level = GameManager.Instance.playerData.critiChanceUpLevel;

        return Mathf.Min(baseCrit + level * perLevel, 1f); // 최대 100% 제한
    }
    void PlayHitEffect(Vector3 position, bool isCritical)//파티클 재생
    {
        ParticleSystem effectPrefab = isCritical ? criticalHitEffect : normalHitEffect;


        // 카메라 앞쪽을 기준으로 위치 보정
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 adjustedPosition = position + cameraForward * 0.5f; // position 기준 앞으로 0.5만큼 이동

        // 파티클 생성
        ParticleSystem instance = Instantiate(effectPrefab, position, Quaternion.identity);

        // 파티클이 Sprite보다 앞에 보이도록 Sorting Order 설정
        var renderer = instance.GetComponent<Renderer>();
        renderer.sortingLayerName = "Default";  // 적 Sprite보다 위에 있는 레이어로 설정
        renderer.sortingOrder = 10;             // Order 높을수록 앞에 보임


        instance.Play();

        Destroy(instance.gameObject, instance.main.duration);
    }
}