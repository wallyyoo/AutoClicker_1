using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;

    public void PlayHitEffect(Vector3 position, bool isCritical)
    {
        ParticleSystem effectPrefab = isCritical ? criticalHitEffect : normalHitEffect;
        if (effectPrefab == null) return;

        Vector3 adjustedPosition = position + Vector3.forward * 0.5f;
        var instance = Instantiate(effectPrefab, adjustedPosition, Quaternion.identity);

        var renderer = instance.GetComponent<Renderer>();
        if (renderer != null)//파티클이 제일 위에 보이게해줌
        {
            renderer.sortingLayerName = "Default";
            renderer.sortingOrder = 10;
        }


        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);
    }
}
