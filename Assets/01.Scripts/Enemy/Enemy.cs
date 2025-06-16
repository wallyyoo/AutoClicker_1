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
    private float moveSpeed = 2f; // �ʿ信 ���� ����
    private Image healthBarImage;
    public int stageIndex;

    private float attackTimer = 0f;

    // ����׿� ������ Ÿ�̸��Դϴ� �ڵ� �ϼ��� �������ּ���.
    private float debugDamageTimer = 0f;
    private int prevDebugSecond = 0;
    // ����׿� ������ Ÿ�̸��Դϴ� �ڵ� �ϼ��� �������ּ���.

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

        /// ����׿� ������ Ÿ�̸��Դϴ� �ڵ� �ϼ��� �������ּ���.
        debugDamageTimer += Time.deltaTime;
        // 1�ʸ��� �α�
        int currentSecond = Mathf.FloorToInt(debugDamageTimer);
        if (currentSecond != prevDebugSecond)
        {
            Debug.Log($"{gameObject.name}��(��) ����� ������ Ÿ�̸Ӹ� ������Ʈ�߽��ϴ�: {debugDamageTimer:F2}��");
            prevDebugSecond = currentSecond;
        }
        if (debugDamageTimer >= 3f)
        {
            Debug.Log($"{gameObject.name}��(��) 1 �������� �޾ҽ��ϴ�.");
            TakeDamage(50);
            debugDamageTimer = 0f;
        }
        /// ����׿� ������ Ÿ�̸��Դϴ� �ڵ� �ϼ��� �������ּ���.
    }

    private void MoveToArrivalPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, arrivalPosition, moveSpeed * Time.deltaTime);// �̵� �ӵ��� ���� ��ġ ������Ʈ
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
        Debug.Log($"{gameObject.name} ����!");
        yield return new WaitForSeconds(data.attackfrequency); // ���� �ִϸ��̼� �ð�
        SetState(EnemyState.Idle);
    }

    public void TakeDamage(int amount)
    {
        if (!isArrived) return; // ���� ������ ���� ������ ����
        currentHealth -= amount;
        UpdateHealthBar(); // ü�¹� ����
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
        // x�� ����(������)���� 1ĭ �˹�, �ʿ�� -1f�� �ٲ㼭 ���ʵ� ����
        Vector2 target = start + Vector2.right * knockbackDistance;

        while (elapsed < knockbackDuration)
        {
            transform.position = Vector2.Lerp(start, target, elapsed / knockbackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;

        SetState(EnemyState.Walk);
        isArrived = false; // �ٽ� �ȱ� ����
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
                    koreanName = "�ϱ޵���";
                    break;
                case EnemyData.EnemyType.Rogue_Grey:
                    koreanName = "�߱޵���";
                    break;
                case EnemyData.EnemyType.Rogue_Samurai:
                    koreanName = "�繫����";
                    break;
                case EnemyData.EnemyType.Rogue_Assassin:
                    koreanName = "��ؽ�";
                    break;
                default:
                    koreanName = "�� �� ����";
                    break;
            }
            enemyNameText.text = $"{koreanName} LV.{stageData.stages[stageIndex].stageKey}";
        }
    }
}