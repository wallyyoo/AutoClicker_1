using UnityEngine;
using System.Collections;

public class AutoAttack : MonoBehaviour
{
    public float attackInterval;//공격 간격
    public bool isAutoAttackEnabled = true;//자동 공격 활성화 여부
    public AttackEffect attackEffect; // <- 연결 필요!

    Coroutine attackRoutine;

    void Start()
    {
        Debug.Log("[AutoAttack] Start 호출됨");
        // 업그레이드 수치 기반으로 공격 간격 계산
        float interval = GameManager.Instance.playerData.UpstatusAutoSpeed;
        attackInterval = interval;

        StartAutoAttack();

    }
    public void StartAutoAttack()
    {
        if (attackRoutine != null)
            StopCoroutine(attackRoutine);

        attackRoutine = StartCoroutine(AutoAttackRoutine());
    }
    IEnumerator AutoAttackRoutine()
    {
        while (isAutoAttackEnabled)
        {
            Debug.Log($"[AutoAttack] 공격 간격: {attackInterval}");
            Attack();
            yield return new WaitForSeconds(attackInterval);//attackInterval(초)만큼 잠시 기다림
        }
    }
    public void RestartAutoAttack()
    {
        StartAutoAttack();
    }
  
    void Attack()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없음");
            return;
        }

        var detector = player.GetComponentInChildren<EnemyDetector>();
        if (detector == null)
        {
            Debug.LogWarning("EnemyDetector 없음");
            return;
        }

        Vector3 attackPos = transform.position + Vector3.right * 3f;
        var playerData = GameManager.Instance.playerData;

        bool isCritical = AttackTest.IsCritical(playerData.UpStatuscriticalChance);
        int damage = AttackTest.CalculateDamage(playerData.UpStatusAttackPower, playerData.UpStatusCriticalDamage, isCritical);

        Enemy target = detector.GetNearestEnemy(attackPos);
        Vector3 effectPos = target != null ? target.transform.position : attackPos;

        if (target != null)
        {
            Debug.Log($"공격! 대상: {target.name}, 데미지: {damage}, 크리티컬: {isCritical}");
            target.TakeDamage(damage);
        }
        else
        {
            Debug.Log("공격했지만 대상 없음");
        }

        attackEffect?.PlayHitEffect(effectPos, isCritical);

    }


    //Debug.Log($"[AutoAttack] 공격 간격: {attackInterval:0.00}초");
    //Debug.Log(isCritical ? $"치명타! 데미지 {damage}" : $"일반 공격 데미지 {damage}");
    float GetCurrentCriticalRate()
    {
        return Mathf.Min(GameManager.Instance.playerData.UpStatuscriticalChance, 1f); // 최대 100%
    }
}
