
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
   public string itemName;
   public string itemDescription;
   
   public int itemCost;
   public int upgradeCost;

   public int attackPower;
   public int criticalDamage;
   public float criticalChance;
   public int  goldGain;
   
   [HideInInspector] public int FinalAttackPower;
   [HideInInspector] public int FinalCriticalDamage;
   [HideInInspector] public float FinalCriticalChance;
   [HideInInspector] public int FinalgoldGain;

   [HideInInspector] public bool isUnlocked;
   [HideInInspector] public bool isEquipped;
   
   
   public void ResetFinalStats()
   {
      FinalAttackPower = attackPower;
      FinalCriticalDamage = criticalDamage;
      FinalCriticalChance = criticalChance;
      FinalgoldGain = goldGain;
   }
}
