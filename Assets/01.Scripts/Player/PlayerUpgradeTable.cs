using UnityEngine;

[CreateAssetMenu(fileName = "PlayerUpgradeTable", menuName = "GameData/PlayerUpgradeTable")]
public class PlayerUpgradeTable : ScriptableObject
{
    [Header("레벨당 고정 증가량")]
    public int attackPowerPerLevel = 10;
    public float critDamagePerLevel = 0.5f;
    public float critChancePerLevel = 0.05f;  // 5%
    public int goldGainPerLevel = 1;
    public float autoSpeedPerLevel = 0.1f;
    public int hpPerLevel = 100;
    public int mpPerLevel = 100;
    public int criticalGoldPerLevel = 100;
    public int autospeedGoldPerLevel = 100;
    public int goldGainGoldPerLevel = 100;
}