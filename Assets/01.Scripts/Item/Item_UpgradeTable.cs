
using UnityEngine;

public static class Item_UpgradeTable
{ 
        //최대 템 레벨
        public static int MaxLevel = 10;

        public static int CalculateUpgradeCost(int baseCost, int level)
        {
            // 업그레이드 비용: (기본 비용 * (현재 레벨))
            return baseCost * level;
        }

        // 계산식
        public static int CalculateAttackPower(int baseAttack, int level)
        {
            return Mathf.RoundToInt(baseAttack * (1 + 0.1f * (level - 1)));
        }

        public static int CalculateCriticalDamage(int baseCriticalDamage, int level)
        {
            return Mathf.RoundToInt(baseCriticalDamage + (5 * (level - 1)));
        }

        public static float CalculateCriticalChance(float baseCriticalChance, int level)
        {
            return baseCriticalChance + (0.01f * (level - 1));
        }

        public static int CalculateGoldGain(int baseGoldGain, int level)
        {
            return Mathf.RoundToInt(baseGoldGain + (baseGoldGain * 0.05f * (level - 1)));
        }
}
