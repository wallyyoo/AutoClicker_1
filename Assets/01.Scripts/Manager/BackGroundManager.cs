using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    public static bool Isbattle = false;
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

    public void BoostAllSpeeds(float multiplier)
    {
        foreach (var bg in backGroundLooper)
        {
            bg.SetMoveSpeed(bg.moveSpeed * multiplier);
        }
    }
    public void ResetAllSpeeds()
    {
        if (Isbattle) return;
        
        foreach (var bg in backGroundLooper)
        {
            
            bg.ResetSpeed();
        }
    }
    
    public void BackGroundAllMoveStop()
    {
        foreach (var bg in backGroundLooper)
        {
            bg.MoveStop();
        }
    }

}


