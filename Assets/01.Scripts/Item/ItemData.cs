
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
   public string itemId;
   public string itemName;
   public int itemCost;
   public int upgradeCost;
   public int attackPower;
   public float criticalChance;

   public Sprite itemIcon;

   public bool isPurchased;
   public bool isEquipped;
   public int upgradeLevel = 1;
}
