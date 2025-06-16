using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public PlayerData_1 playerData;
    public PlayerUpgradeTable playerUpgradeTable;

    public SoundManager soundManager;

    private string path;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (playerData.playerUpgradeTable == null)
            {
                playerData.playerUpgradeTable = playerUpgradeTable;  
            }
            if (soundManager == null)
            {
                soundManager = FindObjectOfType<SoundManager>();
            }

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Json.JsonLoad(); // 무조건 로드
        //Debug.Log("저장 경로: " + Application.persistentDataPath);

        if (Instance != this)
        {
            Debug.Log("중복된 씬을 삭제");
            Destroy(gameObject);
        }
        StartCoroutine(JsonLoadCoroutine());

        // soundManager.startBgm();

    }
    public void JsonSave()
    {
        //// 클래스 데이터를 JSON 문자열로 변환
        //string dataSave = JsonUtility.ToJson(playerData, true);

        //// 해당 경로에 파일 생성 또는 덮어쓰기
        //File.WriteAllText(path, dataSave);

        //Debug.Log($"Save File : {dataSave}");

        Json.JsonSave(); // 경로와 파일 저장은 Json.cs에 위임
    }
    private IEnumerator JsonLoadCoroutine()
    {
        yield return null; // 모든 Awake() 완료 대기

        if (StageManager.Instance == null)
        {
            Debug.Log("StageManager가 초기화되지 않았습니다.");

            // JSON 문자열을 클래스 객체로 변환
            // playerData = JsonUtility.FromJson<PlayerData>(dataLoad);
            //Debug.Log($"Load File : {dataLoad}");

        }
        else
        {
            Debug.Log("초기화 완료");
            Json.JsonLoad(); // 안전하게 실행
        }

    }
}
