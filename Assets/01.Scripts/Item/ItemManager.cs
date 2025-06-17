
using UnityEngine;

// 아이템 구매/장착/업그레이드 상태를 관리하며, 장착 중인 무기의 능력치를 외부에 제공하는 매니저 스크립트
public class ItemManager : MonoBehaviour
{
    
    public static ItemManager ItemManagerInstance;
    
    public ItemData[] allItems;
    public ItemData equippedItem;
    public PlayerData playerData;

    private void Awake()
    {
        if (ItemManagerInstance == null)
        {
            ItemManagerInstance = this;
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
   
    }
    
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
    
    
    public int GetItemAttackPower()
    {
        if (equippedItem == null)
            return 0;

        int level = equippedItem.upgradeLevel;
        return Item_UpgradeTable.CalculateAttackPower(equippedItem.attackPower, level);
    }


    public float GetItemCriticalChance()
    {
        if (equippedItem == null)
            return 0f;

        int level = equippedItem.upgradeLevel;
        return Item_UpgradeTable.CalculateCriticalChance(equippedItem.criticalChance, level);
    }

}
