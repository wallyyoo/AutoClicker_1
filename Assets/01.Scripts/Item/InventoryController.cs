

using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    
    private static InventoryController ItemContrlInstance;
    [Header("인벤토리 창 관련")]
    public GameObject Inventory_OpenButton;
    public GameObject Inventory_CloseButton;
    public GameObject Inventory_Window;
    
    [Header("아이템 상태 버튼")]
    public GameObject Item_BuyButton;
    public GameObject Item_EquipButton;
    public GameObject Item_UpgradeButton;

    [Header("아이템 이미지")]
    public GameObject Item_Image_Enable;
    public GameObject Item_Image_Disable;
    public GameObject Item_Image_Equiped;
    
    [Header("아이템 정보")]
    public TMP_Text Item_Name;
    public TMP_Text Item_Info;
    public TMP_Text Item_BuyCost;
    public TMP_Text Item_UpgradeCost;

    public ItemData currentItem;


    private void Awake()
    {
        if (ItemContrlInstance == null)
        {
            ItemContrlInstance=this; 
       DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public void OpenInventory()
    {
        Inventory_Window.SetActive(true);
        RefreshUI();
    }

    public void CloseInventory()
    {
        Inventory_Window.SetActive(false);
    }

    public void SelectItem(ItemData item)
    {
        currentItem = item;
        RefreshUI();
    }
    public void OnBuyButton()
    {
        ItemManager.ItemManagerInstance.BuyItem(currentItem);
        RefreshUI();
    }

    public void OnEquipButton()
    {
        ItemManager.ItemManagerInstance.EquipItem(currentItem);
        RefreshUI();
    }

    public void OnUpgradeButton()
    {
        ItemManager.ItemManagerInstance.UpgradeItem(currentItem);
        RefreshUI();
    }

    private void RefreshUI()
    {
        Item_Name.text = currentItem.itemName;
        Item_Info.text =
            $"공격력 : {currentItem.attackPower} / 치명타 : {currentItem.criticalChance} %";

        // 버튼 상태 갱신
        bool isUnlocked = ItemManager.ItemManagerInstance.IsUnlocked(currentItem);
        bool isEquipped = ItemManager.ItemManagerInstance.IsEquipped(currentItem);
        bool canUpgrade = ItemManager.ItemManagerInstance.CanUpgrade(currentItem);

        Item_BuyButton.SetActive(!isUnlocked);
        Item_EquipButton.SetActive(isUnlocked && !isEquipped);
        Item_UpgradeButton.SetActive(isEquipped && canUpgrade);

        // 이미지 상태 갱신
        Item_Image_Disable.SetActive(!isUnlocked);
        Item_Image_Enable.SetActive(isUnlocked && !isEquipped);
        Item_Image_Equiped.SetActive(isEquipped);

        // 비용 표시
        Item_BuyCost.text = $"{currentItem.itemCost}G";

        if (isEquipped && canUpgrade)
            Item_UpgradeCost.text = $"{ItemManager.ItemManagerInstance.GetNextUpgradeCost(currentItem)}G";
        else
            Item_UpgradeCost.text = "-";

    }
}
