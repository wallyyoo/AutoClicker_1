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

        // 여기서 초기 간격 설정: PlayerStatData + UpgradeRule 반영
        var baseVal = GameManager.Instance.playerStatData.baseAutoSpeed; // ex: 5초
        var perLevel = GameManager.Instance.playerUpgradeTable.autoSpeedPerLevel;
        var level = GameManager.Instance.playerData.autoSpeedUpLevel;
        attackInterval = Mathf.Max(0.1f, baseVal - level * perLevel); // 최소 0.1초로 제한
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
        Vector3 attackPos = transform.position + Vector3.right * 3f;
        bool isCritical = Random.value < GetCurrentCriticalRate();
        int baseAttack = GameManager.Instance.playerStatData.baseAttackPower;
        int perLevel = GameManager.Instance.playerUpgradeTable.attackPowerPerLevel;
        int level = GameManager.Instance.playerData.attackPowerUpLevel;

        int damage = baseAttack + level * perLevel;
        if (isCritical)
            damage *= 2; // 치명타 시 데미지 2배

        Vector2 boxSize = new Vector2(1.0f, 1.0f);
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPos, boxSize, 0f, LayerMask.GetMask("Enemy"));
        Debug.Log($"적 {hits.Length}명 감지됨");
        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("적에게 데미지 전달됨!");
                Debug.Log($"데미지 : {damage}");
                enemy.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Enemy 스크립트 없음");
            }
        }

        if (attackEffect != null)
            attackEffect.HandleClick(attackPos);

        //Debug.Log($"[AutoAttack] 공격 간격: {attackInterval:0.00}초");
        //Debug.Log(isCritical ? $"치명타! 데미지 {damage}" : $"일반 공격 데미지 {damage}");
    }
    float GetCurrentCriticalRate()
    {
        var baseCrit = GameManager.Instance.playerStatData.baseCriticalChance;
        var perLevel = GameManager.Instance.playerUpgradeTable.critChancePerLevel;
        var level = GameManager.Instance.playerData.critiChanceUpLevel;

        return Mathf.Min(baseCrit + level * perLevel, 1f);
    }
}