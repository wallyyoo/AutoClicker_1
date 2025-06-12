using UnityEngine;
using System.Collections;

public class AutoAttack : MonoBehaviour
{
    public float attackInterval = 1.5f;//공격 간격
    public bool isAutoAttackEnabled = true;//자동 공격 활성화 여부

    void Start()
    {
        StartCoroutine(AutoAttackRoutine());
    }

    IEnumerator AutoAttackRoutine()
    {
        while (isAutoAttackEnabled)
        {
            Attack(); // 나중에 연결
            yield return new WaitForSeconds(attackInterval);//attackInterval(초)만큼 잠시 기다림
        }
    }

    void Attack()
    {
        Debug.Log("자동 공격!");
    }
}