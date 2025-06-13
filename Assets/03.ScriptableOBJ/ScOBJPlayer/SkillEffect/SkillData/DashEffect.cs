using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Effect/DashEffect")]
public class DashEffect : SkillEffectBase, ITemporaryEffect
{
   public int dashSpeed = 5;

   [Header("스킬 이펙트")] public GameObject dashEffectPrefab;


   public override void Execute(GameObject player)
   {
      BackGroundManager.BackInstace.BoostAllSpeeds(dashSpeed);
      Debug.Log("대쉬 스킬을 사용");

      if (dashEffectPrefab != null)
      {
         Vector3 spawnPos = player.transform.position;
         spawnPos.z = 0f;

         GameObject effect = Instantiate(dashEffectPrefab, spawnPos, Quaternion.identity);

         // 💥 핵심: 파티클 시스템 수동 재생 시도
         ParticleSystem ps = effect.GetComponent<ParticleSystem>();
         if (ps != null)
         {
            ps.Play();
         }
         else
         {
            // 자식에 있을 경우 찾아서 재생
            ParticleSystem childPs = effect.GetComponentInChildren<ParticleSystem>();
            if (childPs != null)
            {
               childPs.Play();
            }
            else
            {
               Debug.LogWarning("ParticleSystem을 찾을 수 없습니다. 프리팹에 ParticleSystem이 있는지 확인해주세요.");
            }
         }

         Destroy(effect, 2f);
      }
   }

   public void EndEffect(GameObject caster)
   {
      Debug.Log("원래 속도로 복구");
      BackGroundManager.BackInstace.ResetAllSpeeds();
   }
}
