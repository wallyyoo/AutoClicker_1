using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillEffectBase : ScriptableObject
{
   public abstract void Execute(GameObject player);
}
