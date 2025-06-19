using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public PlayerData PlayerData;
    public List<JsonItem> equipped;
    public List<ItemData> equippeds;
    [HideInInspector] public int curStage;    //(현재 스테이지)
    [HideInInspector] public int curWave;     //(현재 웨이브)
    public SaveData()
    {
        // 초기화
        PlayerData = new PlayerData();
        equipped = new List<JsonItem>();
        curStage = 0;
        curWave = 0;
    }
}
[System.Serializable]
public class JsonItem
{
    public string itemId;
    public string itemName;
    public int itemCost;
    public int upgradeCost;
    public int attackPower;
    public float criticalChance;

    public bool isPurchased;
    public bool isEquipped;
    public int upgradeLevel;

    public JsonItem(ItemData itemData)
    {
        itemId = itemData.itemId;
        itemName = itemData.itemName;
        itemCost = itemData.itemCost;
        upgradeCost = itemData.upgradeCost;
        attackPower = itemData.attackPower;
        criticalChance = itemData.criticalChance;

        isEquipped = itemData.isEquipped;
        isPurchased = itemData.isPurchased;
        upgradeLevel = itemData.upgradeLevel;
    }
}
public static class Json
{
    // 안전한 경로 + 파일 이름 (확장자까지 포함) 지정
    private static string path = Path.Combine(Application.persistentDataPath, "PlayerData.Json");

    [HideInInspector] public static SaveData saveData = new SaveData();
    public static void JsonSave()
    {
        SaveItem();
        
        // saveData가 null이면 새로운 객체로 초기화
        if (saveData == null)
        {
            saveData = new SaveData();
        }
        if (GameManager.Instance != null && GameManager.Instance.playerData != null)
        {
            saveData.PlayerData = GameManager.Instance.playerData;
        }
        if (StageManager.Instance != null)
        {
            saveData.curStage = StageManager.Instance.currentStageIndex;
            saveData.curWave = StageManager.Instance.currentWaveIndex;
        }
        // 클래스 데이터를 JSON 문자열로 변환
        string dataSave = JsonUtility.ToJson(saveData, true);

        // 해당 경로에 파일 생성 또는 덮어쓰기
        File.WriteAllText(path, dataSave);

        Debug.Log($"Save File : {dataSave}");
    }
    public static void JsonLoad()
    {
        if (File.Exists(path))
        {
            // 파일을 읽기
            string json = File.ReadAllText(path);

            if (string.IsNullOrEmpty(json) || json == "{}")
            {
                Debug.LogWarning("Json 파일이 비어있거나 유효하지 않습니다.");
                File.Delete(path);
                return;
            }
            // json파일의 데이터를 savedata에 할당w
            saveData = JsonUtility.FromJson<SaveData>(json);

            GameManager.Instance.playerData = saveData.PlayerData;
            StageManager.Instance.currentStageIndex = saveData.curStage;
            StageManager.Instance.currentWaveIndex = saveData.curWave;

            Debug.Log(JsonUtility.ToJson(saveData, true));
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다. 새로 생성합니다.");
            JsonSave(); // 초기화 저장
        }
    }

    private static void SaveItem()
    {
        saveData.equipped.RemoveAll(item => item.isEquipped);

        //List <ItemData> equippedItems = itemDatas.Where(item => item.isEquipped).ToList()
        foreach (var item in ItemManager.ItemManagerInstance.allItems)
        {

            if (item.isPurchased || item.isEquipped)
            {
                saveData.equipped.Add(new JsonItem(item));
            }
        }
    }

    public static void JsonSaveInit()
    {
        saveData = null;

        JsonSave();
    }
}