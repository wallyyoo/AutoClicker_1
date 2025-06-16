using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public EnemyData data;
    public StageData stageData;

    [SerializeField] private TextMeshProUGUI enemyNameText;
    [SerializeField] private int currentHealth; 
    [SerializeField] private RectTransform currentHealthBarRect;
    [SerializeField] private float maxHealthBarWidth = 100f;

    private int currentDamage;
    private Animator animator;

    private Vector2 arrivalPosition;
    private bool isArrived = false;
    private float moveSpeed = 2f; // 필요에 따라 조정
    private Image healthBarImage;
    public int stageIndex;

    private float attackTimer = 0f;

    // 디버그용 데미지 타이머입니다 코드 완성시 제거해주세요.
    private float debugDamageTimer = 0f;
    private int prevDebugSecond = 0;
    // 디버그용 데미지 타이머입니다 코드 완성시 제거해주세요.

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public enum EnemyState
    {
        Walk,
        Idle,
        Attack,
        Hit,
        Die
    }

    public void Init(EnemyData enemyData, int stageIndex)
    {
        data = enemyData;
        this.stageIndex = stageIndex;
        float healthMultiplier = 1f + stageIndex * 0.1f;
        float damageMultiplier = 1f + stageIndex * 0.1f;

        currentHealth = Mathf.RoundToInt(data.health * healthMultiplier);
        currentDamage = Mathf.RoundToInt(data.damage * damageMultiplier);

        SetState(EnemyState.Walk);
        UpdateHealthBar();
        UpdateEnemyName();
    }

    public void SetArrivalPosition(Vector2 pos)
    {
        arrivalPosition = pos;
        isArrived = false;
    }

    private void Update()
    {
        if (!isArrived)
        {
            MoveToArrivalPosition();
        }
        else
        {
            HandleAttackLoop();
        }

        /// 디버그용 데미지 타이머입니다 코드 완성시 제거해주세요.
        debugDamageTimer += Time.deltaTime;
        // 1초마다 로그
        int currentSecond = Mathf.FloorToInt(debugDamageTimer);
        if (currentSecond != prevDebugSecond)
        {
            Debug.Log($"{gameObject.name}이(가) 디버그 데미지 타이머를 업데이트했습니다: {debugDamageTimer:F2}초");
            prevDebugSecond = currentSecond;
        }
        if (debugDamageTimer >= 3f)
        {
            Debug.Log($"{gameObject.name}이(가) 1 데미지를 받았습니다.");
            TakeDamage(50);
            debugDamageTimer = 0f;
        }
        /// 디버그용 데미지 타이머입니다 코드 완성시 제거해주세요.
    }

    private void MoveToArrivalPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, arrivalPosition, moveSpeed * Time.deltaTime);// 이동 속도에 따라 위치 업데이트
        if (Vector2.Distance(transform.position, arrivalPosition) < 0.05f)
        {
            isArrived = true;
            SetState(EnemyState.Idle);
            attackTimer = 0f;
        }
    }

    private void HandleAttackLoop()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= data.attackfrequency)
        {
            StartCoroutine(AttackRoutine());
            attackTimer = 0f;
        }
    }

    private IEnumerator AttackRoutine()
    {
        SetState(EnemyState.Attack);
        Debug.Log($"{gameObject.name} 공격!");
        yield return new WaitForSeconds(data.attackfrequency); // 공격 애니메이션 시간
        SetState(EnemyState.Idle);
    }

    public void TakeDamage(int amount)
    {
        if (!isArrived) return; // 도착 상태일 때만 데미지 적용
        currentHealth -= amount;
        UpdateHealthBar(); // 체력바 갱신
        if (currentHealth <= 0)
        {
            SetState(EnemyState.Die);
            Die();
            return;
        }
        StartCoroutine(HitAndKnockbackRoutine());
    }

    private IEnumerator HitAndKnockbackRoutine()
    {
        SetState(EnemyState.Hit);

        float knockbackDuration = 0.4f;
        float knockbackDistance = 1f;
        float elapsed = 0f;
        Vector2 start = transform.position;
        // x축 방향(오른쪽)으로 1칸 넉백, 필요시 -1f로 바꿔서 왼쪽도 가능
        Vector2 target = start + Vector2.right * knockbackDistance;

        while (elapsed < knockbackDuration)
        {
            transform.position = Vector2.Lerp(start, target, elapsed / knockbackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;

        SetState(EnemyState.Walk);
        isArrived = false; // 다시 걷기 시작
    }

    private void Die()
    {
        Destroy(gameObject, 1.0f);
        EnemyManager.Instance.OnEnemyDied(this);
    }
    private void SetState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Walk:
                animator.SetTrigger("Walk");
                break;
            case EnemyState.Idle:
                animator.SetTrigger("Idle");
                break;
            case EnemyState.Attack:
                animator.SetTrigger("Attack");
                break;
            case EnemyState.Hit:
                animator.SetTrigger("Hit");
                break;
            case EnemyState.Die:
                animator.SetTrigger("Die");
                break;
        }
    }

    private void UpdateHealthBar()
    {
        if (currentHealthBarRect == null) return;
        float percent = Mathf.Clamp01((float)currentHealth / data.health);
        currentHealthBarRect.sizeDelta = new Vector2(maxHealthBarWidth * percent, currentHealthBarRect.sizeDelta.y);
    }

    private void UpdateEnemyName()
    {
        if (stageData != null && stageData.stages.Count > stageIndex)
        {
            string koreanName = "";
            switch (data.enemyType)
            {
                case EnemyData.EnemyType.Rogue_Brown:
                case EnemyData.EnemyType.Rogue_Green:
                case EnemyData.EnemyType.Rogue_Blue:
                    koreanName = "하급도적";
                    break;
                case EnemyData.EnemyType.Rogue_Grey:
                    koreanName = "중급도적";
                    break;
                case EnemyData.EnemyType.Rogue_Samurai:
                    koreanName = "사무라이";
                    break;
                case EnemyData.EnemyType.Rogue_Assassin:
                    koreanName = "어쌔신";
                    break;
                default:
                    koreanName = "알 수 없음";
                    break;
            }
            enemyNameText.text = $"{koreanName} LV.{stageData.stages[stageIndex].stageKey}";
        }
    }
}