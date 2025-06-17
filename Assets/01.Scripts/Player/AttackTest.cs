using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackTest
{
    public static bool IsCritical(float critRate)
    {
        return Random.value < Mathf.Clamp01(critRate);//1이 넘어갔다면 1반환 이후 랜덤 작동
    }
    public static int CalculateDamage(int baseDamage, float critMultiplier, bool isCritical)
    {
        return isCritical ? Mathf.RoundToInt(baseDamage * critMultiplier) : baseDamage;//true면 치명타 데미지 계산, false면 그냥 기본 데미지 반환
    }
    //public static Enemy FindNearestEnemy(Collider2D[] hits, Vector3 fromPos)//fromPos 기준위치
    //{
    //    float minDistance = float.MaxValue;//가장 가까운 적을 찾기 위해, minDistance를 아주 큰 값으로 설정
    //    Enemy nearest = null;
    //    foreach (var hit in hits)//감지된 적들을 하나씩 확인함
    //    {
    //        var enemy = hit.GetComponent<Enemy>();//콜라이더에 붙어 있는 Enemy 스크립트를 찾음
    //        if (enemy != null && enemy.isArrived)//Enemy 컴포넌트가 있고, isArrived가 true인 적만 대상으로 함
    //        {
    //            float dist = Vector2.Distance(fromPos, enemy.transform.position);//fromPos 위치와 enemy 위치 사이의 직선 거리(유클리드 거리)를 구해주는 함수
    //            if (dist < minDistance)//지금까지 중 가장 가까운 적이라면, nearest로 저장
    //            {
    //                minDistance = dist;
    //                nearest = enemy;
    //            }
    //        }
    //    }
    //    return nearest;
    //}

    ////플레이어의 BoxCollider2D 중심 좌표와 실제 크기(size)를 계산해서 돌려주는 함수
    //// player → 박스를 찾을 기준 GameObject(보통은 플레이어)
    ////out Vector2 center → 콜라이더 중심 위치를 담을 출력값
    ////out Vector2 size → 콜라이더의 실제 크기를 담을 출력값
    //public static void GetColliderBox(GameObject player, out Vector2 center, out Vector2 size)
    //{
    //    //기본값 설정: 혹시 실패할 경우 대비
    //    center = Vector2.zero;
    //    size = Vector2.one;

    //    //플레이어 자신 혹은 자식 중에서 BoxCollider2D를 찾고, 없으면 경고만 찍고 함수 종료
    //    var box = player.GetComponentInChildren<BoxCollider2D>();
    //    if (box == null)
    //    {
    //        Debug.LogWarning("BoxCollider2D 없음");
    //        return;
    //    }

    //    //box.transform.positio 콜라이더가 붙어있는 게임오브젝트의 월드 위치
    //    //box.offset 콜라이더의 로컬 오프셋
    //    //즉, 이 둘을 더하면 → 월드 좌표계에서 콜라이더의 실제 중심 위치
    //    center = (Vector2)box.transform.position + box.offset;

    //    //box.size는 로컬 스케일 기준의 크기 (즉, 원본 사이즈)
    //    //lossyScale은 월드 기준으로 최종 스케일이 얼마나 곱해졌는지 알려줌
    //    //곱한값이 화면상에서 실제 보이는 크기
    //    size = new Vector2(
    //        box.size.x * box.transform.lossyScale.x,
    //        box.size.y * box.transform.lossyScale.y
    //    );
    //}

}