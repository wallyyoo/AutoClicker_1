using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackEffect : MonoBehaviour
{
    public ParticleSystem normalHitEffect;
    public ParticleSystem criticalHitEffect;


    private void OnDisable()
    {
        ClickManager.OnClick -= OnClick;
    }
    private void OnEnable()
    {
        ClickManager.OnClick += OnClick;
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
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다!");
            return;
        }

        var detector = player.GetComponentInChildren<EnemyDetector>();
        if (detector == null)
        {
            Debug.LogWarning("EnemyDetector 없음!");
            return;
        }


        // 플레이어 데이터 가져오기
        var playerData = GameManager.Instance.playerData;

        // 치명타 판정 및 데미지 계산
        bool isCritical = AttackTest.IsCritical(playerData.UpStatuscriticalChance);
        int damage = AttackTest.CalculateDamage(playerData.UpStatusAttackPower, playerData.UpStatusCriticalDamage, isCritical);

        // 가장 가까운 적 찾기
        Enemy target = detector.GetNearestEnemy(clickPosition);
        Vector3 effectPos = target != null ? target.transform.position : clickPosition;

        if (target != null)
        {
            target.TakeDamage(damage);
            Debug.Log($"[ClickAttack] 적 '{target.name}'에게 {damage} 데미지 적용됨 (치명타: {isCritical})");
        }
        else
        {
            Debug.Log("[ClickAttack] 감지된 적이 없어서 데미지 없음");
        }

        PlayHitEffect(effectPos, isCritical);
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
