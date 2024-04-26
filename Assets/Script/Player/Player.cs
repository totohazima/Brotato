using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int characterNum;
    public string characterCode;
    public string characterName;
    [Header("Stat")]
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

    [HideInInspector] public int consumableHeal;
    [HideInInspector] public float magnetRange;
    [HideInInspector] public float expGain;
    [HideInInspector] public int penetrate;
    [HideInInspector] public float instantMagnet;
    [HideInInspector] public float KnockBack;
    [HideInInspector] public float explosiveDamage;
    [HideInInspector] public float penetrateDamage;
    [HideInInspector] public float explosiveSize;
    [HideInInspector] public int chain;
    [HideInInspector] public float bossDamage;


    [HideInInspector] public float maxHealth_Origin;
    [HideInInspector] public float regeneration_Origin;
    [HideInInspector] public float bloodSucking_Origin;
    [HideInInspector] public float persentDamage_Origin;
    [HideInInspector] public float meleeDamage_Origin;
    [HideInInspector] public float rangeDamage_Origin;
    [HideInInspector] public float attackSpeed_Origin;
    [HideInInspector] public float criticalChance_Origin;
    [HideInInspector] public float engine_Origin;
    [HideInInspector] public float range_Origin;
    [HideInInspector] public float armor_Origin;
    [HideInInspector] public float evasion_Origin;
    [HideInInspector] public float accuracy_Origin;
    [HideInInspector] public float speed_Origin;

    [HideInInspector] public int consumableHeal_Origin;
    [HideInInspector] public float magnetRange_Origin;
    [HideInInspector] public float expGain_Origin;
    [HideInInspector] public int penetrate_Origin;
    [HideInInspector] public float instantMagnet_Origin;
    [HideInInspector] public float KnockBack_Origin;
    [HideInInspector] public float explosiveDamage_Origin;
    [HideInInspector] public float penetrateDamage_Origin;
    [HideInInspector] public float explosiveSize_Origin;
    [HideInInspector] public int chain_Origin;
    [HideInInspector] public float bossDamage_Origin;

    [Header("ITEM")]
    public List<Item> itemInventory;
    public List<GameObject> weapons;
    public enum Character
    {
        VERSATILE, //다재다능
    }

    public void StatSetting(int index)
    {
        PlayerStatInfoTable.Data import = GameManager.instance.gameDataBase.playerStatInfoTable.table[index];

        characterNum = index;
        characterCode = import.playerCode;
        characterName = import.name;
        maxHealth_Origin = import.health;
        regeneration_Origin = import.hpRegen;
        bloodSucking_Origin = import.bloodSucking;
        persentDamage_Origin = import.persentDamage;
        meleeDamage_Origin = import.meleeDamage;
        rangeDamage_Origin = import.rangeDamage;
        attackSpeed_Origin = import.attackSpeed;
        criticalChance_Origin = import.criticalChance;
        engine_Origin = import.engine;
        range_Origin = import.range;
        armor_Origin = import.armor;
        evasion_Origin = import.evasion;
        accuracy_Origin = import.accuracy;
        speed_Origin = import.speed;

        maxHealth = import.health;

        StatCalculate();
    }

    public void StatCalculate()
    {
        if (ItemEffect.instance != null)
        {
            ItemEffect.instance.CountCheck();
        }

        //스탯 초기화
        StatReset();
        void StatReset()
        {
            maxHealth = maxHealth_Origin;
            regeneration = regeneration_Origin;
            bloodSucking = bloodSucking_Origin;
            persentDamage = persentDamage_Origin;
            meleeDamage = meleeDamage_Origin;
            rangeDamage = rangeDamage_Origin;
            attackSpeed = attackSpeed_Origin;
            criticalChance = criticalChance_Origin;
            engine = engine_Origin;
            range = range_Origin;
            armor = armor_Origin;
            evasion = evasion_Origin;
            accuracy = accuracy_Origin;
            speed = speed_Origin;
            consumableHeal = consumableHeal_Origin;
            magnetRange = magnetRange_Origin;
            expGain = expGain_Origin;
            penetrate = penetrate_Origin;
            instantMagnet = instantMagnet_Origin;
            KnockBack = KnockBack_Origin;
            explosiveDamage = explosiveDamage_Origin;
            penetrateDamage = penetrateDamage_Origin;
            explosiveSize = explosiveSize_Origin;
            chain = chain_Origin;
            bossDamage = bossDamage_Origin;
        }

        //아이템 스탯 계산
        for (int i = 0; i < itemInventory.Count; i++)
        {
            for (int k = 0; k < itemInventory[i].curCount; k++)
            {
                for (int j = 0; j < itemInventory[i].riseCount; j++)
                {
                    switch (itemInventory[i].riseStat[j])
                    {
                        case Stat.PlayerStat.MAXHEALTH:
                            maxHealth += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.REGENERATION:
                            regeneration += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BLOOD_SUCKING:
                            bloodSucking += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PERSENT_DAMAGE:
                            persentDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MELEE_DAMAGE:
                            meleeDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE_DAMAGE:
                            rangeDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ATTACK_SPEED:
                            attackSpeed += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CRITICAL_CHANCE:
                            criticalChance += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENGINE:
                            engine += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE:
                            range += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ARMOR:
                            armor += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EVASION:
                            evasion += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ACCURACY:
                            accuracy += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.SPEED:
                            speed += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CONSUMABLE_HEAL:
                            consumableHeal += (int)itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MAGNET_RANGE:
                            magnetRange += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXP_GAIN:
                            expGain += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRATE:
                            penetrate += (int)itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.INSTNAT_MAGNET:
                            instantMagnet += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.KNOCK_BACK:
                            KnockBack += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_DAMAGE:
                            explosiveDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRTE_DAMAGE:
                            penetrateDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_SIZE:
                            explosiveSize += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CHAIN:
                            chain += (int)itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BOSS_DAMAGE:
                            bossDamage += itemInventory[i].riseStats[j];
                            break;
                    }
                }
                for (int j = 0; j < itemInventory[i].decreaseCount; j++)
                {
                    switch (itemInventory[i].decreaseStat[j])
                    {
                        case Stat.PlayerStat.MAXHEALTH:
                            maxHealth -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.REGENERATION:
                            regeneration -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.BLOOD_SUCKING:
                            bloodSucking -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PERSENT_DAMAGE:
                            persentDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.MELEE_DAMAGE:
                            meleeDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE_DAMAGE:
                            rangeDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ATTACK_SPEED:
                            attackSpeed -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CRITICAL_CHANCE:
                            criticalChance -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ENGINE:
                            engine -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE:
                            range -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ARMOR:
                            armor -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EVASION:
                            evasion -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ACCURACY:
                            accuracy -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.SPEED:
                            speed -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CONSUMABLE_HEAL:
                            consumableHeal -= (int)itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.MAGNET_RANGE:
                            magnetRange -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXP_GAIN:
                            expGain -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRATE:
                            penetrate -= (int)itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.INSTNAT_MAGNET:
                            instantMagnet -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.KNOCK_BACK:
                            KnockBack -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_DAMAGE:
                            explosiveDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRTE_DAMAGE:
                            penetrateDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_SIZE:
                            explosiveSize -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CHAIN:
                            chain -= (int)itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.BOSS_DAMAGE:
                            bossDamage -= itemInventory[i].decreaseStats[j];
                            break;
                    }
                }
            }
        }

        //무기 세트 스탯 계산
        SettCalculate();
        void SettCalculate()
        {
            if (WeaponManager.instance != null)
            {
                WeaponManager sett = WeaponManager.instance;

                //비무장 = 회피
                if (sett.unArmed_Set == 2)
                {
                    evasion += 3;
                }
                else if (sett.unArmed_Set == 3)
                {
                    evasion += 6;
                }
                else if (sett.unArmed_Set == 4)
                {
                    evasion += 9;
                }
                else if (sett.unArmed_Set == 5)
                {
                    evasion += 12;
                }
                else if (sett.unArmed_Set >= 6)
                {
                    evasion += 15;
                }

                //도구 = 엔지니어링
                if (sett.tool_Set == 2)
                {
                    engine += 1;
                }
                else if (sett.tool_Set == 3)
                {
                    engine += 2;
                }
                else if (sett.tool_Set == 4)
                {
                    engine += 3;
                }
                else if (sett.tool_Set == 5)
                {
                    engine += 4;
                }
                else if (sett.tool_Set >= 6)
                {
                    engine += 5;
                }

                //총 = 범위
                if (sett.gun_Set == 2)
                {
                    range += 10;
                }
                else if (sett.gun_Set == 3)
                {
                    range += 20;
                }
                else if (sett.gun_Set == 4)
                {
                    range += 30;
                }
                else if (sett.gun_Set == 5)
                {
                    range += 40;
                }
                else if (sett.gun_Set >= 6)
                {
                    range += 50;
                }

                //폭발물 = 폭발 크기
                if (sett.explosive_Set == 2)
                {
                    explosiveSize += 5;
                }
                else if (sett.explosive_Set == 3)
                {
                    explosiveSize += 10;
                }
                else if (sett.explosive_Set == 4)
                {
                    explosiveSize += 15;
                }
                else if (sett.explosive_Set == 5)
                {
                    explosiveSize += 20;
                }
                else if (sett.explosive_Set >= 6)
                {
                    explosiveSize += 25;
                }

                //정확 = 치명타율
                if (sett.precision_Set == 2)
                {
                    criticalChance += 3;
                }
                else if (sett.precision_Set == 3)
                {
                    criticalChance += 6;
                }
                else if (sett.precision_Set == 4)
                {
                    criticalChance += 9;

                }
                else if (sett.precision_Set == 5)
                {
                    criticalChance += 12;
                }
                else if (sett.precision_Set >= 6)
                {
                    criticalChance += 15;
                }

                //원시 = 체력
                if (sett.native_Set == 2)
                {
                    maxHealth += 3;
                }
                else if (sett.native_Set == 3)
                {
                    maxHealth += 6;
                }
                else if (sett.native_Set == 4)
                {
                    maxHealth += 9;
                }
                else if (sett.native_Set == 5)
                {
                    maxHealth += 12;
                }
                else if (sett.native_Set >= 6)
                {
                    maxHealth += 15;
                }
            }
        }

    }
}
