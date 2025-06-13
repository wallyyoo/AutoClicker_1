using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public SkillData[] equippedSkills;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSkill(0);
        }
       
    }

    void UseSkill(int index)
    {
        SkillData skill = equippedSkills[index];
        Debug.Log($"스킬 사용 {skill.skillName}");
    }
}
