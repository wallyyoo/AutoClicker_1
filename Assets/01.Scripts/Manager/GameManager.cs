using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public PlayerDataList playerDataList;

    private string path;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 안전한 경로 + 파일 이름 (확장자까지 포함) 지정
        path = Path.Combine(Application.persistentDataPath, "PlayerData.Json");
        JsonLoad();
    }

    public void JsonSave()
    {
        // 클래스 데이터를 JSON 문자열로 변환
        string dataSave = JsonUtility.ToJson(playerDataList, true);

        // 해당 경로에 파일 생성 또는 덮어쓰기
        File.WriteAllText(path, dataSave);

        Debug.Log($"Save File : {dataSave}");
    }

    public void JsonLoad()
    {
        if (File.Exists(path))
        {
            // 파일이 존재한다면 읽어서 JSON 문자열로 가져옴
            string dataLoad =  File.ReadAllText(path);

            // JSON 문자열을 클래스 객체로 변환
            playerDataList = JsonUtility.FromJson<PlayerDataList>(dataLoad);
        }
        else
        {
            // 폴더가 없다면 새로운 객체를 만들어서 초기화 후 Save
            playerDataList = new PlayerDataList();
            JsonSave();
        }
    }
}
