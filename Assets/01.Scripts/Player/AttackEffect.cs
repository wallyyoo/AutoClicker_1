using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;
    //public float criticalRate = 0.2f;
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
        //bool isCritical = IsCriticalHit();
        bool isCritical = Random.value < critRate;
        PlayHitEffect(position, isCritical);
        Debug.Log(isCritical ? $"치명타! 확률: {critRate * 100:F1}%" : $"일반 공격 (치명타 확률: {critRate * 100:F1}%)");
        //Debug.Log(isCritical ? $"치명타!{UpgradeUI.criticalRate}" : "일반 공격");
    }
    //bool IsCriticalHit()//크리티컬 확인
    //{ 
    //return Random.value < UpgradeUI.criticalRate;
    //}
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
        ParticleSystem instance = Instantiate(effectPrefab, position, Quaternion.identity);
        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);
    }
}