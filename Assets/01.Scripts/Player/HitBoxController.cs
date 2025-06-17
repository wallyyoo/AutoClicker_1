using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
   
   private bool monsterCheking = false;
   

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
      {
         monsterCheking = true;
         if (monsterCheking == true)
         {
            BackGroundManager.BackInstace.BackGroundAllMoveStop();
            Debug.Log("백그라운드용 히트박스 몬스터 체킹, 적 감지됨");
            
         }
      }
   }

   private void OnTriggerExit2D(Collider2D other)
   {
      if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
      {
         monsterCheking = false;
         if (monsterCheking == false)
         {
            BackGroundManager.BackInstace.ResetAllSpeeds();
            Debug.Log("백그라운드용 히트박스 몬스터 체킹, 적이 사라짐");
         }
      }
   }
}
