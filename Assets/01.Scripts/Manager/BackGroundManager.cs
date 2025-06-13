using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField] public BackGroundLooper[] backGroundLooper;
    
    private static BackGroundManager _backinstance;

    public static BackGroundManager BackInstace
    {
        get
        {
            if (_backinstance == null)
            {
                _backinstance = FindObjectOfType<BackGroundManager>();
            }

            return _backinstance;
        }
    }
    
    void Awake()
    {
        if (_backinstance == null)
        {
            _backinstance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
        else if (_backinstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ChanceMoveSpeed(float dashMoveSpeed)
    {
        Debug.Log("테스트");
        foreach (var bg in backGroundLooper)
        {
            Debug.Log("체인스피드 메서드");
            bg.moveSpeed *= dashMoveSpeed;
            
        }
    }
    
}


