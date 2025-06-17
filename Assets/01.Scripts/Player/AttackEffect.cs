using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;

    private void OnEnable()
    {
        ClickManager.OnClick += OnClick;
    }

    private void OnDisable()
    {
        ClickManager.OnClick -= OnClick;
    }

   private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(_boxCenter.x, _boxCenter.y), new Vector3(_boxSize.x, _boxSize.y, 10f));
    }

    private Vector2 _boxSize;
    private Vector2 _boxCenter;

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
        //  플레이어 자식 오브젝트 중 BoxCollider2D 가져오기
        BoxCollider2D boxCollider = player.GetComponentInChildren<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogWarning("플레이어에 BoxCollider2D가 없습니다.");
            return;
        }
        //  콜라이더 중심 위치 및 스케일 반영한 크기 계산
        Vector2 boxCenter = (Vector2)boxCollider.transform.position + boxCollider.offset;
        Vector2 boxSize = new Vector2(
            boxCollider.size.x * boxCollider.transform.lossyScale.x,
            boxCollider.size.y * boxCollider.transform.lossyScale.y
        );

        _boxCenter = boxCenter;
        _boxSize = boxSize;
        Collider2D[] hits = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0f, LayerMask.GetMask("Enemy"));
       

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
                    return;
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
            Debug.Log(" 데미지를 받은 적이 없습니다.");
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
