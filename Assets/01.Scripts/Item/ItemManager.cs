
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public static ItemManager Instance;
    
    public ItemData[] allItems;
    public ItemData equippedItem;
    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BuyItem(ItemData item)
    {
        if (!item.isUnlocked && playerData.curGold >= item.itemCost)
        {
            playerData.curGold -= item.itemCost;
            item.isUnlocked = true;
        }
    }

    public void EquipItem(ItemData item)
    {
        if (!item.isUnlocked)
        {
            return;
        }

        if (equippedItem != null)
        {
            equippedItem.isEquipped = false;
        }
        equippedItem = item;
        equippedItem.isEquipped = true;

        // ApplyItemStats();
    }

    public void UpgradeItem(ItemData item)
    {
        if (!item.isEquipped || item.upgradeLevel >= Item_UpgradeTable.MaxLevel)
            return;

        int cost = Item_UpgradeTable.CalculateUpgradeCost(item.upgradeCost, item.upgradeLevel + 1);
        if (playerData.curGold < cost)
            return;

        playerData.curGold -= cost;
        item.upgradeLevel++;
        // ApplyItemStats();
    }
    
    // private void ApplyItemStats()
    // {
    //     if (equippedItem == null)
    //         return;
    //
    //     int level = equippedItem.upgradeLevel;
    //
    //     playerData.FinalAattackPower = Item_UpgradeTable.CalculateAttackPower(equippedItem.attackPower, level);
    //     playerData.FinalCriticalDamage = Item_UpgradeTable.CalculateCriticalDamage(equippedItem.criticalDamage, level);
    //     playerData.FinalCriticalChance = Item_UpgradeTable.CalculateCriticalChance(equippedItem.criticalChance, level);
    //     playerData.FinalGoldGain = Item_UpgradeTable.CalculateGoldGain(equippedItem.goldGain, level);
    // }
    
    // 아이템의 구매, 착용 여부의 상태를 체크
    public bool IsUnlocked(ItemData item)
    {
        return item.isUnlocked;
    }

    public bool IsEquipped(ItemData item)
    {
        return item.isEquipped;
    }

    public bool CanUpgrade(ItemData item)
    {
        return item.isEquipped && item.upgradeLevel < Item_UpgradeTable.MaxLevel;
    }

    public int GetNextUpgradeCost(ItemData item)
    {
        return Item_UpgradeTable.CalculateUpgradeCost(item.upgradeCost, item.upgradeLevel + 1);
    }

}
