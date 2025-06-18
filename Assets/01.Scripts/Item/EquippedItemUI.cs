using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EquippedItemUI : MonoBehaviour
{
    public ItemManager itemManager;  // 인스펙터에서 연결
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public Image itemIconImage;

    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        var equipped = itemManager.equippedItem;

        int finalAttack = Item_UpgradeTable.CalculateAttackPower(equipped.attackPower, equipped.upgradeLevel);
        float finalCrit = Item_UpgradeTable.CalculateCriticalChance(equipped.criticalChance, equipped.upgradeLevel) * 100f;
        
        if (equipped != null)
        {
            itemNameText.text = equipped.itemName;
            itemDescriptionText.text =
                $"Lv.{equipped.upgradeLevel}\n" +
                $"ATK: {finalAttack}\n" +
                $"CRI: {finalCrit:F1}%";
            
            if (equipped.itemIcon != null)
            {
                itemIconImage.sprite = equipped.itemIcon;
                itemIconImage.enabled = true;
            }
            else
            {
                itemIconImage.enabled = false; // 아이콘 없는 경우 숨기기
            }
        }
        else
        {
            itemNameText.text = "장착 없음";
            itemDescriptionText.text = "-";
            itemIconImage.enabled = false;
        }
    }
}