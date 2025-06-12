
using System;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    
    private static InventoryController instance;
    
    public GameObject Inventory_OpenButton;
    public GameObject Inventory_CloseButton;
    public GameObject Inventory_Window;
    public GameObject Item_BuyButton;
    public GameObject Item_EquipButton;
    public GameObject Item_UpgradeButton;

    public GameObject Item_Image_Enable;
    public GameObject Item_Image_Disable;
    public GameObject Item_Image_Equiped;
    
    public TMP_Text Item_Name;
    public TMP_Text Item_Info;
    public TMP_Text Item_BuyCost;
    public TMP_Text Item_UpgradeCost;


    //private int money;

    private void Awake()
    {
       if(instance==null)
                    
         instance=this; 
       DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
/* #if UNITY_SW
        money = 10000;
#else
        money = GameManager.Instance.GetMoney();
#endif
        Debug.Log(money);*/
    }

    public void OpenInventory()
    {
        Inventory_OpenButton.SetActive(false);
        Inventory_Window.SetActive(true);
        Item_Image_Enable.SetActive(false);
        Item_Image_Disable.SetActive(true);
    }

    public void CloseInventory()
    {
        Inventory_Window.SetActive(false);
        Inventory_OpenButton.SetActive(true);
    }

    public void BuyItem()
    {
        Item_BuyButton.SetActive(false);
        Item_EquipButton.SetActive(true);
        Item_Image_Disable.SetActive(false);
        Item_Image_Enable.SetActive(true);
    }

    public void EquipItem()
    {
        Item_EquipButton.SetActive(false);
        Item_UpgradeButton.SetActive(true);
        Item_Image_Enable.SetActive(false);
        Item_Image_Equiped.SetActive(true);
        
    }
 
    
    
  
}
