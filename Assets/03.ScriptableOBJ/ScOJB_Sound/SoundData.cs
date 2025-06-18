using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SoundData", menuName = "Audio/SoundData")]
public class SoundData : ScriptableObject
{
   public string soundName;
   public AudioClip clip;
   public float volume;
   public float pitch;
   public bool loop;
}
