
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
        Debug.Log("ItemManager Awake_1");
        Debug.Log($"ItemManager playerData instance: {playerData.GetHashCode()}");
        if (ItemManagerInstance == null)
        {
            ItemManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Debug.Log("ItemManager Start_1");

        foreach (var item in allItems)
        {
            if (item.isEquipped)
            {
                equippedItem = item;
                break;
            }
        }
        Debug.Log("ItemManager Start_2");

    }

    public void BuyItem(ItemData item)
    {
    
        if (item.isPurchased)
        {
            Debug.Log($"{item.itemName} 이미 구매된 아이템입니다.");
            return;
        }

        if (playerData.curGold >= item.itemCost)
        {
            playerData.curGold -= item.itemCost;
            item.isPurchased = true;
            Debug.Log($"{item.itemName} 구매 성공. 남은 골드: {playerData.curGold}");
        }
        else
        {
            Debug.Log($"Gold 부족, 구매 불가. 현재 보유 골드: {playerData.curGold}");
        }
    }

    public void EquipItem(ItemData item)
    {
        if (!item.isPurchased)
        {
            Debug.Log("장착 실패: 잠금 해제되지 않은 아이템입니다.");
            return;
        }

        if (equippedItem != null)
        {
            equippedItem.isEquipped = false;
            Debug.Log($"기존 장착 해제: {equippedItem.itemName}");
        }

        equippedItem = item;
        equippedItem.isEquipped = true;

        Debug.Log($"{item.itemName} 장착 완료.");

    }

    public void UpgradeItem(ItemData item)
    {
        if (!item.isEquipped)
        {
            Debug.Log("업그레이드 실패: 장착된 아이템이 아닙니다.");
            return;
        }

        if (item.upgradeLevel >= Item_UpgradeTable.MaxLevel)
        {
            Debug.Log("최대 레벨입니다.");
            return;
        }

        int cost = Item_UpgradeTable.CalculateUpgradeCost(item.upgradeCost, item.upgradeLevel + 1);
        if (playerData.curGold < cost)
        {
            Debug.Log($"Gold 부족, 업그레이드 불가. 필요 골드: {cost}, 현재 골드: {playerData.curGold}");
            return;
        }

        playerData.curGold -= cost;
        item.upgradeLevel++;

        Debug.Log($"{item.itemName} 업그레이드 성공. 현재 레벨: {item.upgradeLevel}, 남은 골드: {playerData.curGold}");
   
    }
    
    // 아이템의 구매, 착용 여부의 상태를 체크
    public bool IsUnlocked(ItemData item)
    {
        return item.isPurchased;
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
