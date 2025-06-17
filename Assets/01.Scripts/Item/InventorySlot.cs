
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 아이템 슬롯에서 구매/장착/업그레이드 기능을 담당하는 슬롯 전용 스크립트
public class InventorySlot : MonoBehaviour
{
    [Header("이미지 상태")]
    public GameObject imageDisable;
    public GameObject imageEnable;
    public GameObject imageEquipped;

    [Header("아이템 정보")]
    public TMP_Text itemNameText;
    public TMP_Text itemInfoText;

    [Header("버튼")]
    public Button buyButton;
    public Button equipButton;
    public Button upgradeButton;

    private ItemData itemData;

    public void Setup(ItemData data)
    {
        itemData = data;

        itemNameText.text = itemData.itemName;
        itemInfoText.text = $"공격력:{itemData.attackPower} \n크리:{itemData.criticalChance * 100}%";
        
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
        
        bool isUnlocked = ItemManager.ItemManagerInstance.IsUnlocked(itemData);
        bool isEquipped = ItemManager.ItemManagerInstance.IsEquipped(itemData);
        bool canUpgrade = ItemManager.ItemManagerInstance.CanUpgrade(itemData);
        
        imageDisable.SetActive(!isUnlocked);
        imageEnable.SetActive(isUnlocked && !isEquipped);
        imageEquipped.SetActive(isEquipped);
        
        buyButton.gameObject.SetActive(!isUnlocked);
        equipButton.gameObject.SetActive(isUnlocked && !isEquipped);
        upgradeButton.gameObject.SetActive(isEquipped && canUpgrade);
        
    }

    private void OnBuy()
    {
        ItemManager.ItemManagerInstance.BuyItem(itemData);
        Refresh();
    }

    private void OnEquip()
    {
        ItemManager.ItemManagerInstance.EquipItem(itemData);
        Refresh();
    }

    private void OnUpgrade()
    {
        ItemManager.ItemManagerInstance.UpgradeItem(itemData);
        Refresh();
    }
}
