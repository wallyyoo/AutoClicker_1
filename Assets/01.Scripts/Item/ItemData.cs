
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
   public string itemName;
   public int itemCost;
   public int upgradeCost;
   public int attackPower;
   public int criticalDamage;
   public float criticalChance;
   public int goldGain;

   [HideInInspector] public bool isUnlocked;
   [HideInInspector] public bool isEquipped;
   [HideInInspector] public int upgradeLevel = 1;
   

}
