using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/SkillData")]
public class SkillScriptableObject : ScriptableObject
{
   public string skillName;
   public Sprite skillIcon;
   public float coolDown;

   [TextArea]
   public string description;

   public SkillEffectBase effect;
}
