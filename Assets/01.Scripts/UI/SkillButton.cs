using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
   public SkillScriptableObject[] equippedSkills;
   public GameObject player;
   
   public int skillIndex;
   
   public Image imgIcon;
   public Image imgCool;
   public GameObject cooldownOBJ;

   private SkillScriptableObject currentSkill;
   private bool isCoolDown;


   void Start()
   {
      // 시작 시 이 버튼이 사용할 스킬 정보 가져오기
      currentSkill = equippedSkills[skillIndex];

      // 아이콘 및 쿨타임 초기화
      imgIcon.sprite = currentSkill.skillIcon;
      imgCool.fillAmount = 0f;
      cooldownOBJ.SetActive(false);
   }
   
   public void UseSkill()
   {
      if (currentSkill == null) return;
      if (isCoolDown) return;
      
      
      isCoolDown = true;
      // 스킬 효과 실행
      currentSkill.effect.Execute(player);
      
      // 쿨다운 시작
      cooldownOBJ.SetActive(true);
      StartCoroutine(SkillCoolDown());

      Debug.Log($"스킬 사용: {currentSkill.skillName}");
   }
   
   IEnumerator SkillCoolDown()
   {
      float duration = currentSkill.coolDown; // 총 쿨타임
      float timer = 0f;                       // 경과 시간

      imgCool.fillAmount = 1f;                // 시작 시 100% (쿨타임 시작)

      while (timer < duration)
      {
         timer += Time.deltaTime;
         // 남은 비율 = 1 - (경과 시간 / 총 시간)
         imgCool.fillAmount = 1 - (timer / duration);

         yield return null;
      }

      // 쿨다운 종료
      imgCool.fillAmount = 0f;
      cooldownOBJ.SetActive(false);
      isCoolDown = false;
      
      if (currentSkill.effect is ITemporaryEffect tempEffect)
      {
         tempEffect.EndEffect(player);
      }
   }

}
