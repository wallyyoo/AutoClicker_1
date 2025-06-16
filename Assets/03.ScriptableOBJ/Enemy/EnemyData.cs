using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public enum EnemyType
    {
        Rogue_Brown,
        Rogue_Green,
        Rogue_Blue,
        Rogue_Grey,
        Rogue_Samurai,
        Rogue_Assassin,
    }
    public EnemyType enemyType;
    public int reward;
    public int health;
    public int damage;
    public float attackfrequency;
}
