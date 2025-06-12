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
        bool isCritical = IsCriticalHit();

        PlayHitEffect(position, isCritical);

        Debug.Log(isCritical ? $"치명타!{UpgradeUI.criticalRateTest}" : "일반 공격");
    }
    bool IsCriticalHit()//크리티컬 확인
    { 
    return Random.value < UpgradeUI.criticalRateTest;
    }
    void PlayHitEffect(Vector3 position, bool isCritical)//파티클 재생
    {
        ParticleSystem effectPrefab = isCritical ? criticalHitEffect : normalHitEffect;
        ParticleSystem instance = Instantiate(effectPrefab, position, Quaternion.identity);
        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);
    }
}