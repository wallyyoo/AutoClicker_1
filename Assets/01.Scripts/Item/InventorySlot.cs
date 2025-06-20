
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 인벤토리 슬롯 UI에서 아이템 정보 표시 및 구매/장착/업그레이드 처리
/// </summary>
public class InventorySlot : MonoBehaviour
{
    [Header("아이템 상태 이미지")]
    public GameObject imageDisable;
    public GameObject imageEnable;
    public GameObject imageEquipped;

    [Header("아이템 정보")]
    public Image itemIconImage;
    public TMP_Text itemNameText;
    public TMP_Text itemInfoText;
    public TMP_Text upgradeCostText;
    public TMP_Text buyCostText;

    [Header("버튼")]
    public Button buyButton;
    public Button equipButton;
    public Button upgradeButton;

    private ItemData itemData;
    

    public void Setup(ItemData data)
    {
        itemData = data;
        buyButton.onClick.RemoveAllListeners();
        equipButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.RemoveAllListeners();

        buyButton.onClick.AddListener(OnBuy);
        equipButton.onClick.AddListener(OnEquip);
        upgradeButton.onClick.AddListener(OnUpgrade);

        Refresh();
    }
    public void Refresh()
    {
        
        int finalAttack = Item_UpgradeTable.CalculateAttackPower(itemData.attackPower, itemData.upgradeLevel);
        float finalCrit = Item_UpgradeTable.CalculateCriticalChance(itemData.criticalChance, itemData.upgradeLevel) * 100f;
        
        bool isUnlocked = ItemManager.ItemManagerInstance.IsUnlocked(itemData);
        bool isEquipped = ItemManager.ItemManagerInstance.IsEquipped(itemData);
        bool canUpgrade = ItemManager.ItemManagerInstance.CanUpgrade(itemData);
        
        itemIconImage.sprite = itemData.itemIcon;
        itemIconImage.enabled = true;

        imageDisable.SetActive(!isUnlocked);
        imageEnable.SetActive(isUnlocked && !isEquipped);
        imageEquipped.SetActive(isEquipped);

        buyButton.gameObject.SetActive(!isUnlocked);
        equipButton.gameObject.SetActive(isUnlocked && !isEquipped);
        upgradeButton.gameObject.SetActive(isEquipped && canUpgrade);

        
        if (!isUnlocked)
        {
            itemNameText.text = "???";
            itemInfoText.text = "???";
            buyCostText.text = $"G{itemData.itemCost}";
        }
        else
        {
            itemNameText.text = itemData.itemName;
            itemInfoText.text =
                $"Lv.{itemData.upgradeLevel}\n" +
                $"ATT: {finalAttack}\n" +
                $"CRI: {finalCrit:F1}%";
            buyCostText.text = "";
        }

        if (upgradeButton.gameObject.activeSelf)
        {
            int nextCost = ItemManager.ItemManagerInstance.GetNextUpgradeCost(itemData);
            upgradeCostText.text = $"G{nextCost}";
        }
        
    }

    private void OnBuy()
    {
        ItemManager.ItemManagerInstance.BuyItem(itemData);
        InventoryController.ItemControlInstance.RefreshAllSlots();
    }

    private void OnEquip()
    {
        ItemManager.ItemManagerInstance.EquipItem(itemData);
        InventoryController.ItemControlInstance.RefreshAllSlots();
    }

    private void OnUpgrade()
    {
        ItemManager.ItemManagerInstance.UpgradeItem(itemData);
        InventoryController.ItemControlInstance.RefreshAllSlots();
    }
}
