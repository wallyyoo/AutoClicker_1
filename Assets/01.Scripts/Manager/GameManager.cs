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
        path = Path.Combine(Application.persistentDataPath, "PlayerData");
        JsonLoad();
    }

    public void JsonSave()
    {
        string dataSave = JsonUtility.ToJson(playerDataList, true);
        File.WriteAllText(path, dataSave);

        Debug.Log($"Save File : {dataSave}");
    }

    public void JsonLoad()
    {
        if (File.Exists(path))
        {
            string dataLoad =  File.ReadAllText(path);
            playerDataList = JsonUtility.FromJson<PlayerDataList>(dataLoad);
        }
        else
        {
            playerDataList = new PlayerDataList();
        }
    }
}
