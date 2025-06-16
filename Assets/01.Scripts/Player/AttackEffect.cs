using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;

    private void OnEnable()
    {
        Debug.Log("AttackEffect 스크립트가 활성화되었습니다.");
        ClickManager.OnClick += OnClick;
    }

    private void OnDisable()
    {
        ClickManager.OnClick -= OnClick;
    }

    private void OnClick(Vector3 clickPosition)
    {
        bool isCritical = Random.value < GetCurrentCriticalRate();
        // 1. 클릭 위치에도 이펙트 출력
        PlayHitEffect(clickPosition, isCritical);
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다!");
            return;
        }

        Vector2 playerPos = player.transform.position;

        Vector2 boxSize = new Vector2(5.0f, 5.0f);

        Collider2D[] hits = Physics2D.OverlapBoxAll(playerPos, boxSize, 0f, LayerMask.GetMask("Enemy"));

        int damage = GameManager.Instance.playerData.UpStatusAttackPower;
        if (isCritical)
        {
            float critMultiplier = GameManager.Instance.playerData.UpStatusCriticalDamage;
            damage = Mathf.RoundToInt(damage * critMultiplier);
        }

        int hitCount = 0;
        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.isArrived)
                {
                    enemy.TakeDamage(damage);
                    PlayHitEffect(enemy.transform.position, isCritical);
                    Debug.Log($"→ 적 '{enemy.name}' 에게 {damage} 데미지 적용됨");
                    hitCount++;
                }
                else
                {
                    Debug.Log($"→ 적 '{enemy.name}' 은 도착하지 않아 데미지 없음");
                }
            }
            else
            {
                Debug.Log("→ 감지된 객체에 Enemy 컴포넌트 없음");
            }
        }

        if (hitCount == 0)
        {
            Debug.Log("📌 데미지를 받은 적이 없습니다.");
        }
    }
    public void PlayEffectAt(Vector3 worldPos, bool isCritical)
    {
        PlayHitEffect(worldPos, isCritical);
    }

    public void PlayHitEffect(Vector3 position, bool isCritical)
    {
        ParticleSystem effectPrefab = isCritical ? criticalHitEffect : normalHitEffect;

        if (effectPrefab == null)
        {
            Debug.LogWarning("파티클 프리팹이 지정되지 않았습니다.");
            return;
        }

        Vector3 adjustedPosition = position + (Camera.main?.transform.forward ?? Vector3.forward) * 0.5f;

        ParticleSystem instance = Instantiate(effectPrefab, adjustedPosition, Quaternion.identity);

        var renderer = instance.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = "Default";
            renderer.sortingOrder = 10;
        }

        instance.Play();
        Destroy(instance.gameObject, instance.main.duration);
    }

    private float GetCurrentCriticalRate()
    {
        return Mathf.Clamp01(GameManager.Instance.playerData.UpStatuscriticalChance);
    }
}
