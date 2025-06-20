using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// 인벤토리 UI를 생성하고 관리하는 컨트롤러
/// </summary>
public class InventoryController : MonoBehaviour
{
    public static InventoryController ItemControlInstance { get; private set; }

    [Header("인벤토리 창 관련")] 
    public GameObject Inventory_Window;

    [Header("슬롯 생성 관련")] 
    public Transform Inventory_content; // 인벤토리 슬롯 
    public GameObject Inventory_Item_preset; // 인벤토리에 있는 아이템 프리셋


    private void Awake()
    {
        if (ItemControlInstance == null)
        {
            ItemControlInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(DelayedGenerateInventory());
    }

    private IEnumerator DelayedGenerateInventory()
    {
        yield return null;

        if (ItemManager.ItemManagerInstance == null)
        {
            Debug.LogError("ItemManager.Instance 가 초기화되지 않았습니다.");
            yield break;
        }
        GenerateInventory();
    }


    void GenerateInventory() // SO 데이터를 순회하며 슬롯 프리팹을 생성 및 초기화
    {
        foreach (Transform child in Inventory_content)
            Destroy(child.gameObject);

        foreach (ItemData data in ItemManager.ItemManagerInstance.allItems)
        {
            GameObject slotObj = Instantiate(Inventory_Item_preset, Inventory_content);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.Setup(data);
        }
    }

    public void RefreshAllSlots()
    {
        foreach (Transform child in Inventory_content)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null)
                slot.Refresh();
        }
    }
    public void OpenInventory()
    {
        Inventory_Window.SetActive(true);
    }

    public void CloseInventory()
    {
        Inventory_Window.SetActive(false);
    }
}