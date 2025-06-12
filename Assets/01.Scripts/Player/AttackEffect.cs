using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;
    public float criticalRate = 0.2f;

    void OnEnable()
    {
        ClickManager.OnClick += HandleClick;
    }

    void OnDisable()
    {
        ClickManager.OnClick -= HandleClick;
    }

    void HandleClick(Vector3 position)
    {
        bool isCritical = Random.value < criticalRate;

        ParticleSystem effectPrefab = isCritical ? criticalHitEffect : normalHitEffect;
        ParticleSystem instance = Instantiate(effectPrefab, position, Quaternion.identity);
        instance.Play(); // 명시적으로 재생 (필요 시)
        Destroy(instance.gameObject, instance.main.duration); // 자동으로 꺼지도록 설정
        Debug.Log(isCritical ? "치명타!" : "일반 공격");
    }
}