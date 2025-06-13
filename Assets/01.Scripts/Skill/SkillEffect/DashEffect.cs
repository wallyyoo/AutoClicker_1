using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Effect/DashEffect")]
public class DashEffect : SkillEffectBase
{
   public int dashSpeed = 5;

   public override void Execute(GameObject player)
   {
      BackGroundManager.BackInstace.ChanceMoveSpeed(dashSpeed);
      Debug.Log("대쉬 스킬을 사용");
   }
}
