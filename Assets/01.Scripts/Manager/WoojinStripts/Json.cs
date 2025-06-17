using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public PlayerData PlayerData;

    [HideInInspector] public int curStage;    //(현재 스테이지)
    [HideInInspector] public int curWave;     //(현재 웨이브)
}

public static class Json
{
    // 안전한 경로 + 파일 이름 (확장자까지 포함) 지정
    private static string path = Path.Combine(Application.persistentDataPath, "PlayerData.Json");

    [HideInInspector] public static SaveData saveData;
    public static void JsonSave()
    {            
        // saveData 초기화
        saveData = new SaveData()
        {
            // 값을 덮어 쓰기
            PlayerData = GameManager.Instance.playerData,
            curStage = StageManager.Instance.currentStageIndex,
            curWave = StageManager.Instance.currentWaveIndex
        };

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
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // savedata에 있는 값을 덮어 씌우기
            GameManager.Instance.playerData = saveData.PlayerData;
            StageManager.Instance.currentStageIndex = saveData.curStage;
            StageManager.Instance.currentWaveIndex = saveData.curWave;

            Debug.Log(JsonUtility.ToJson(saveData, true));
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다. 새로 생성합니다.");
            GameManager.Instance.playerData = new PlayerData();
            JsonSave(); // 초기화 저장
        }
    }
}

