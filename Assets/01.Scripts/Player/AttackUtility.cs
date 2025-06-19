
using UnityEngine;

public static class AttackUtility
{
    public static bool IsCritical(float critRate)
    {
        return Random.value < Mathf.Clamp01(critRate);//1이 넘어갔다면 1반환 이후 랜덤 작동
    }
    public static int CalculateDamage(int baseDamage, float critMultiplier, bool isCritical)
    {
        return isCritical ? Mathf.RoundToInt(baseDamage * critMultiplier) : baseDamage;//true면 치명타 데미지 계산, false면 그냥 기본 데미지 반환
    }
  

}