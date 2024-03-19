using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("STAT")]
    public int characterNum;
    public string characterName;
    public float maxHealth;
    public float regeneration;
    public float bloodSucking;
    public float persentDamage;
    public float meleeDamage;
    public float rangeDamage;
    public float attackSpeed;
    public float criticalChance;
    public float engine;
    public float range;
    public float armor;
    public float evasion;
    public float accuracy;
    public float speed;

    public int consumableHeal;
    public float magnetRange;
    public float expGain;
    public int penetrate;
    public float instantMagnet;
    public float KnockBack;
    public float explosiveDamage;
    public float penetrateDamage;
    [Header("ITEM")]
    public List<Item> itemInventory;
    public List<GameObject> weapons;
    public enum Character
    {
        VERSATILE, //다재다능
    }

    public void StatSetting(int index)
    {
        PlayerStatImporter import = PlayerStatImporter.instance;

        characterNum = import.characterNum[index];
        characterName = import.characterName[index];
        maxHealth = import.maxHealth[index];
        regeneration = import.regeneration[index];
        bloodSucking = import.bloodSucking[index];
        persentDamage = import.persentDamage[index];
        meleeDamage = import.meleeDamage[index];
        rangeDamage = import.rangeDamage[index];
        attackSpeed = import.attackSpeed[index];
        criticalChance = import.criticalChance[index];
        engine = import.engine[index];
        range = import.range[index];
        armor = import.armor[index];
        evasion = import.evasion[index];
        accuracy = import.accuracy[index];
        speed = import.speed[index];
    }

    public void StatCalculate(Item calculateItem)
    {
        ItemEffect.instance.CountCheck();

        Item item = calculateItem;

        //if((int)item.itemType == 35)
        //{
        //    ItemEffect.instance.isWeirdGhost = true;
        //}

        for (int j = 0; j < item.riseCount; j++)
        {
            switch ((int)item.riseStat[j])
            {
                case 1:
                    maxHealth += item.riseStats[j];
                    break;
                case 2:
                    regeneration += item.riseStats[j];
                    break;
                case 3:
                    bloodSucking += item.riseStats[j];
                    break;
                case 4:
                    persentDamage += item.riseStats[j];
                    break;
                case 5:
                    meleeDamage += item.riseStats[j];
                    break;
                case 6:
                    rangeDamage += item.riseStats[j];
                    break;
                case 7:
                    attackSpeed += item.riseStats[j];
                    break;
                case 8:
                    criticalChance += item.riseStats[j];
                    break;
                case 9:
                    engine += item.riseStats[j];
                    break;
                case 10:
                    range += item.riseStats[j];
                    break;
                case 11:
                    armor += item.riseStats[j];
                    break;
                case 12:
                    evasion += item.riseStats[j];
                    break;
                case 13:
                    accuracy += item.riseStats[j];
                    break;
                case 14:
                    speed += item.riseStats[j];
                    break;
                case 15:
                    consumableHeal += (int)item.riseStats[j];
                    break;
                case 16:
                    magnetRange += item.riseStats[j];
                    break;
                case 17:
                    expGain += item.riseStats[j];
                    break;
                case 18:
                    penetrate += (int)item.riseStats[j];
                    break;
                case 19:
                    instantMagnet += item.riseStats[j];
                    break;
                case 20:
                    KnockBack += item.riseStats[j];
                    break;
                case 21:
                    explosiveDamage += item.riseStats[j];
                    break;
                case 22:
                    penetrateDamage += item.riseStats[j];
                    break;
            }
        }
        for (int j = 0; j < item.descendCount; j++)
        {
            switch ((int)item.descendStat[j])
            {
                case 1:
                    maxHealth -= item.descendStats[j];
                    break;
                case 2:
                    regeneration -= item.descendStats[j];
                    break;
                case 3:
                    bloodSucking -= item.descendStats[j];
                    break;
                case 4:
                    persentDamage -= item.descendStats[j];
                    break;
                case 5:
                    meleeDamage -= item.descendStats[j];
                    break;
                case 6:
                    rangeDamage -= item.descendStats[j];
                    break;
                case 7:
                    attackSpeed -= item.descendStats[j];
                    break;
                case 8:
                    criticalChance -= item.descendStats[j];
                    break;
                case 9:
                    engine -= item.descendStats[j];
                    break;
                case 10:
                    range -= item.descendStats[j];
                    break;
                case 11:
                    armor -= item.descendStats[j];
                    break;
                case 12:
                    evasion -= item.descendStats[j];
                    break;
                case 13:
                    accuracy -= item.descendStats[j];
                    break;
                case 14:
                    speed -= item.descendStats[j];
                    break;
                case 15:
                    consumableHeal -= (int)item.descendStats[j];
                    break;
                case 16:
                    magnetRange -= item.descendStats[j];
                    break;
                case 17:
                    expGain -= item.descendStats[j];
                    break;
                case 18:
                    penetrate -= (int)item.descendStats[j];
                    break;
                case 19:
                    instantMagnet -= item.descendStats[j];
                    break;
                case 20:
                    KnockBack -= item.descendStats[j];
                    break;
                case 21:
                    explosiveDamage -= item.descendStats[j];
                    break;
                case 22:
                    penetrateDamage -= item.descendStats[j];
                    break;
            }
        }
    }
}
