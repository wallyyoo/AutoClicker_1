﻿using System.Collections;
using UnityEngine;
/// <summary>
/// 게임의 전반적인 상태와 초기화를 담당하는 매니저
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerData playerData;
    public SoundManager soundManager;

    private string path;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

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
       
        if (Instance != this)
        {
            Debug.Log("중복된 씬을 삭제");
            Destroy(gameObject);
        }
        StartCoroutine(JsonLoadCoroutine());
        
    }
    private IEnumerator JsonLoadCoroutine()
    {
        Debug.Log("JsonLoadCoroutine_1");
        yield return null; // 모든 Awake() 완료 대기
        Debug.Log("JsonLoadCoroutine_2");
        if (StageManager.Instance == null)
        {
            Debug.Log("StageManager가 초기화되지 않았습니다.");
        }
        else
        {
            Debug.Log("초기화 완료");
            Json.JsonLoad(); // 안전하게 실행
            
        }
        UIMainManager.Instance.RefreshCurGold();
    }
}
