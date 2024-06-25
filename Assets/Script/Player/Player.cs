using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int characterNum;
    public string characterCode;
    public string characterName;
    [Header("Basic_Stat")]
    //public int playerLevel; //플레이어 레벨
    public float maxHealth;
    public float regeneration;
    public float bloodSucking;
    public float persentDamage;
    public float meleeDamage;
    public float rangeDamage;
    public float elementalDamage;
    public float attackSpeed;
    public float criticalChance;
    public float engine;
    public float range;
    public float armor;
    public float evasion;
    public float accuracy;
    public float lucky;
    public float harvest;
    public float speed;

    [Header("Detail_Stat")]
    public float consumableHeal;
    public float meterialHeal;
    public float expGain;
    public float magnetRange;
    public float priceSale;
    public float explosiveDamage;
    public float explosiveSize;
    public int chain;
    public int penetrate;
    public float penetrateDamage;
    public float bossDamage;
    public float knockBack;
    public float doubleMeterial;
    public float lootInMeterial;
    public float freeReroll;
    public float tree;
    public float enemyAmount;
    public float enemySpeed;
    public float instantMagnet;
    public Stat.ItemTag[] characterItemTags;

    [HideInInspector] public float maxHealth_Origin;
    [HideInInspector] public float regeneration_Origin;
    [HideInInspector] public float bloodSucking_Origin;
    [HideInInspector] public float persentDamage_Origin;
    [HideInInspector] public float meleeDamage_Origin;
    [HideInInspector] public float rangeDamage_Origin;
    [HideInInspector] public float elementalDamage_Origin;
    [HideInInspector] public float attackSpeed_Origin;
    [HideInInspector] public float criticalChance_Origin;
    [HideInInspector] public float engine_Origin;
    [HideInInspector] public float range_Origin;
    [HideInInspector] public float armor_Origin;
    [HideInInspector] public float evasion_Origin;
    [HideInInspector] public float accuracy_Origin;
    [HideInInspector] public float lucky_Origin;
    [HideInInspector] public float harvest_Origin;
    [HideInInspector] public float speed_Origin;

    [HideInInspector] public float consumableHeal_Origin;
    [HideInInspector] public float meterialHeal_Origin;
    [HideInInspector] public float expGain_Origin;
    [HideInInspector] public float magnetRange_Origin;
    [HideInInspector] public float priceSale_Origin;
    [HideInInspector] public float explosiveDamage_Origin;
    [HideInInspector] public float explosiveSize_Origin;
    [HideInInspector] public int chain_Origin;
    [HideInInspector] public int penetrate_Origin;
    [HideInInspector] public float penetrateDamage_Origin;
    [HideInInspector] public float bossDamage_Origin;
    [HideInInspector] public float knockBack_Origin;
    [HideInInspector] public float doubleMeterial_Origin;
    [HideInInspector] public float lootInMeterial_Origin;
    [HideInInspector] public float freeReroll_Origin;
    [HideInInspector] public float tree_Origin;
    [HideInInspector] public float enemyAmount_Origin;
    [HideInInspector] public float enemySpeed_Origin;
    [HideInInspector] public float instantMagnet_Origin;

    [Header("Inventory")]
    public List<Item> itemInventory;
    public List<Weapon_Action> weapons;
    public enum Character
    {
        WELLROUNDED, //다재다능
        RANGER, //레인저
        PACIFIST, //평화주의자
        MULTITASKER, //멀티태스커
        GLADIATOR, //검투사
        ENGINEER, //엔지니어
        BULL, //황소
        SOLDIER, //군인
        TEST1,
        TEST2,
        TEST3,
    }

    public void StatSetting(int index)
    {
        PlayerStatInfoTable.Data import = GameManager.instance.gameDataBase.playerStatInfoTable.table[index];

        characterNum = index;
        characterCode = import.playerCode.ToString();
        characterName = import.name;
        maxHealth_Origin = import.health;
        regeneration_Origin = import.hpRegen;
        bloodSucking_Origin = import.bloodSucking;
        persentDamage_Origin = import.persentDamage;
        meleeDamage_Origin = import.meleeDamage;
        rangeDamage_Origin = import.rangeDamage;
        elementalDamage_Origin = import.elementalDamage;
        attackSpeed_Origin = import.attackSpeed;
        criticalChance_Origin = import.criticalChance;
        engine_Origin = import.engine;
        range_Origin = import.range;
        armor_Origin = import.armor;
        evasion_Origin = import.evasion;
        accuracy_Origin = import.accuracy;
        lucky_Origin = import.lucky;
        harvest_Origin = import.harvest;
        speed_Origin = import.speed;

        maxHealth = import.health;

        consumableHeal_Origin = import.consumableHeal;
        meterialHeal_Origin = import.meterialHeal;
        expGain_Origin = import.expGain;
        magnetRange_Origin = import.magnetRange;
        priceSale_Origin = import.priceSale;
        explosiveDamage_Origin = import.explosiveDamage;
        explosiveSize_Origin = import.explosiveSize;
        chain_Origin = import.chain;
        penetrate_Origin = import.penetrate;
        penetrateDamage_Origin = import.penetrateDamage;
        bossDamage_Origin = import.bossDamage;
        knockBack_Origin = import.knockBack;
        doubleMeterial_Origin = import.doubleMeterial;
        lootInMeterial_Origin = import.lootInMeterial;
        freeReroll_Origin = import.freeReroll;
        tree_Origin = import.tree;
        enemyAmount_Origin = import.enemyAmount;
        enemySpeed_Origin = import.enemySpeed;
        instantMagnet_Origin = import.instantMagnet;

        characterItemTags = new Stat.ItemTag[import.itemTags.Length];
        for (int i = 0; i < import.itemTags.Length; i++)
        {
            characterItemTags[i] = import.itemTags[i];
        }
        GameManager.instance.playerInfo.WeaponSetSearch();
        StatCalculate();
    }

    public void StatCalculate()
    {
        if (GameManager.instance.playerAct != null)
            GameManager.instance.playerInfo.ItemSearch();

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
            elementalDamage = elementalDamage_Origin;
            attackSpeed = attackSpeed_Origin;
            criticalChance = criticalChance_Origin;
            engine = engine_Origin;
            range = range_Origin;
            armor = armor_Origin;
            evasion = evasion_Origin;
            accuracy = accuracy_Origin;
            lucky = lucky_Origin;
            harvest = harvest_Origin;
            speed = speed_Origin;

            consumableHeal = consumableHeal_Origin;
            meterialHeal = meterialHeal_Origin;
            expGain = expGain_Origin;
            magnetRange = magnetRange_Origin;
            priceSale = priceSale_Origin;
            explosiveDamage = explosiveDamage_Origin;
            explosiveSize = explosiveSize_Origin;
            chain = chain_Origin;
            penetrate = penetrate_Origin;
            penetrateDamage = penetrateDamage_Origin;
            bossDamage = bossDamage_Origin;
            knockBack = knockBack_Origin;
            doubleMeterial = doubleMeterial_Origin;
            lootInMeterial = lootInMeterial_Origin;
            freeReroll = freeReroll_Origin;
            tree = tree_Origin;
            enemyAmount = enemyAmount_Origin;
            enemySpeed = enemySpeed_Origin;
            instantMagnet = instantMagnet_Origin;
        }

        //레벨 당 체력 + 1
        maxHealth += GameManager.instance.playerInfo.playerLevel;

        PlayerInfo info = GameManager.instance.playerInfo;
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
                            if (GameManager.instance.character == Character.RANGER) //레인저 체력 증가량 -25%
                                maxHealth += itemInventory[i].riseStats[j] * 0.75f;
                            else
                                maxHealth += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.REGENERATION:
                            if (GameManager.instance.character == Character.BULL) //황소 재생 증가량 +50%
                                regeneration += itemInventory[i].riseStats[j] * 1.5f;
                            else
                                regeneration += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BLOOD_SUCKING:
                            bloodSucking += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PERSENT_DAMAGE:
                            if (GameManager.instance.character == Character.ENGINEER) //엔지니어 대미지 증가량 -50%
                                persentDamage += itemInventory[i].riseStats[j] * 0.5f;
                            else
                                persentDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MELEE_DAMAGE:
                            meleeDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.RANGE_DAMAGE:
                            if (GameManager.instance.character == Character.RANGER) //레인저 원거리 대미지 증가량 +50%
                                rangeDamage += itemInventory[i].riseStats[j] * 1.5f;
                            else
                                rangeDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ELEMENTAL_DAMAGE:
                            elementalDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ATTACK_SPEED:
                            attackSpeed += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CRITICAL_CHANCE:
                            criticalChance += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENGINE:
                            if (GameManager.instance.character == Character.ENGINEER) //엔지니어 엔지니어링 증가량 +25%
                                engine += itemInventory[i].riseStats[j] * 1.25f;
                            else
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
                        case Stat.PlayerStat.LUCKY:
                            lucky += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.HARVEST:
                            harvest += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.SPEED:
                            speed += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CONSUMABLE_HEAL:
                            consumableHeal += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.METERIAL_HEAL:
                            meterialHeal += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXP_GAIN:
                            expGain += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.MAGNET_RANGE:
                            magnetRange += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PRICE_SALE:
                            priceSale += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_DAMAGE:
                            explosiveDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_SIZE:
                            explosiveSize += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.CHAIN:
                            chain += (int)itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRATE:
                            penetrate += (int)itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRTE_DAMAGE:
                            penetrateDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.BOSS_DAMAGE:
                            bossDamage += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.KNOCK_BACK:
                            knockBack += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.DOUBLE_METERIAL:
                            doubleMeterial += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.LOOT_IN_METERIAL:
                            lootInMeterial += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.FREE_REROLL:
                            freeReroll += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.TREE:
                            tree += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_AMOUNT:
                            enemyAmount += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_SPEED:
                            enemySpeed += itemInventory[i].riseStats[j];
                            break;
                        case Stat.PlayerStat.INSTNAT_MAGNET:
                            instantMagnet += itemInventory[i].riseStats[j];
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
                        case Stat.PlayerStat.ELEMENTAL_DAMAGE:
                            elementalDamage -= itemInventory[i].decreaseStats[j];
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
                        case Stat.PlayerStat.LUCKY:
                            lucky -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.HARVEST:
                            harvest -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.SPEED:
                            speed -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CONSUMABLE_HEAL:
                            consumableHeal -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.METERIAL_HEAL:
                            meterialHeal -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXP_GAIN:
                            expGain -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.MAGNET_RANGE:
                            magnetRange -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PRICE_SALE:
                            priceSale -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_DAMAGE:
                            explosiveDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.EXPLOSIVE_SIZE:
                            explosiveSize -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.CHAIN:
                            chain -= (int)itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRATE:
                            penetrate -= (int)itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.PENETRTE_DAMAGE:
                            penetrateDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.BOSS_DAMAGE:
                            bossDamage -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.KNOCK_BACK:
                            knockBack -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.DOUBLE_METERIAL:
                            doubleMeterial -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.LOOT_IN_METERIAL:
                            lootInMeterial -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.FREE_REROLL:
                            freeReroll -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.TREE:
                            tree -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_AMOUNT:
                            enemyAmount -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.ENEMY_SPEED:
                            enemySpeed -= itemInventory[i].decreaseStats[j];
                            break;
                        case Stat.PlayerStat.INSTNAT_MAGNET:
                            instantMagnet -= itemInventory[i].decreaseStats[j];
                            break;
                    }
                }
            }
        }

        //무기 세트 스탯 계산
        SettCalculate();
        void SettCalculate()
        {
            GameManager game = GameManager.instance;
            float riseStat = 0;
            //비무장 = 회피
            if (game.playerInfo.unArmed_Set == 2)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.unArmed_Set == 3)
            {
                riseStat = 6;
            }
            else if (game.playerInfo.unArmed_Set == 4)
            {
                riseStat = 9;
            }
            else if (game.playerInfo.unArmed_Set == 5)
            {
                riseStat = 12;
            }
            else if (game.playerInfo.unArmed_Set >= 6)
            {
                riseStat = 15;
            }
            evasion += riseStat;
            riseStat = 0;
            //도구 = 엔지니어링
            if (game.playerInfo.tool_Set == 2)
            {
                riseStat = 1;
            }
            else if (game.playerInfo.tool_Set == 3)
            {
                riseStat = 2;
            }
            else if (game.playerInfo.tool_Set == 4)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.tool_Set == 5)
            {
                riseStat = 4;
            }
            else if (game.playerInfo.tool_Set >= 6)
            {
                riseStat = 5;
            }
            if (GameManager.instance.character == Character.ENGINEER) //엔지니어 엔지니어링 증가량 +25%
                engine += riseStat * 1.25f;
            else
                engine += riseStat;
            riseStat = 0;
            //총 = 범위
            if (game.playerInfo.gun_Set == 2)
            {
                riseStat = 10;
            }
            else if (game.playerInfo.gun_Set == 3)
            {
                riseStat = 20;
            }
            else if (game.playerInfo.gun_Set == 4)
            {
                riseStat = 30;
            }
            else if (game.playerInfo.gun_Set == 5)
            {
                riseStat = 40;
            }
            else if (game.playerInfo.gun_Set >= 6)
            {
                riseStat = 50;
            }
            range += riseStat;
            riseStat = 0;
            //폭발물 = 폭발 크기
            if (game.playerInfo.explosive_Set == 2)
            {
                riseStat = 5;
            }
            else if (game.playerInfo.explosive_Set == 3)
            {
                riseStat = 10;
            }
            else if (game.playerInfo.explosive_Set == 4)
            {
                riseStat = 15;
            }
            else if (game.playerInfo.explosive_Set == 5)
            {
                riseStat = 20;
            }
            else if (game.playerInfo.explosive_Set >= 6)
            {
                riseStat = 25;
            }
            explosiveSize += riseStat;
            riseStat = 0;
            //정확 = 치명타율
            if (game.playerInfo.precision_Set == 2)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.precision_Set == 3)
            {
                riseStat = 6;
            }
            else if (game.playerInfo.precision_Set == 4)
            {
                riseStat = 9;

            }
            else if (game.playerInfo.precision_Set == 5)
            {
                riseStat = 12;
            }
            else if (game.playerInfo.precision_Set >= 6)
            {
                riseStat = 15;
            }
            criticalChance += riseStat;
            riseStat = 0;
            //원시 = 체력
            if (game.playerInfo.native_Set == 2)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.native_Set == 3)
            {
                riseStat = 6;
            }
            else if (game.playerInfo.native_Set == 4)
            {
                riseStat = 9;
            }
            else if (game.playerInfo.native_Set == 5)
            {
                riseStat = 12;
            }
            else if (game.playerInfo.native_Set >= 6)
            {
                riseStat = 15;
            }
            if (GameManager.instance.character == Character.RANGER) //레인저 최대 체력 증가량 -25%
                maxHealth += riseStat * 0.75f;
            else
                maxHealth += riseStat;
            riseStat = 0;
            //원소 = 원소 대미지
            if (game.playerInfo.elemental_Set == 2)
            {
                riseStat = 1;
            }
            else if (game.playerInfo.elemental_Set == 3)
            {
                riseStat = 2;
            }
            else if (game.playerInfo.elemental_Set == 4)
            {
                riseStat = 3;
            }
            else if (game.playerInfo.elemental_Set == 5)
            {
                riseStat = 4;
            }
            else if (game.playerInfo.elemental_Set >= 6)
            {
                riseStat = 5;
            }
            elementalDamage += riseStat;
            riseStat = 0;
        }

        harvest *= 1 + (GameManager.instance.harvestVariance_Amount / 100);
        harvest = Mathf.Round(harvest);

        if(GameManager.instance.character == Character.MULTITASKER)
        {
            persentDamage -= 5 * weapons.Count;
        }
        if(GameManager.instance.character == Character.GLADIATOR)
        {
            bool isSame = false;
            List<Weapon.Weapons> sortWeapon = new List<Weapon.Weapons>();
            for (int i = 0; i < weapons.Count; i++)
            {
                Weapon_Action myWeapon = weapons[i].GetComponent<Weapon_Action>();

                for (int j = 0; j < sortWeapon.Count; j++)
                {
                    if(myWeapon.index == sortWeapon[j])
                    {
                        isSame = true;
                    }
                }
                //같은 무기가 없을 경우
                if(isSame == false)
                {
                    sortWeapon.Add(myWeapon.index);
                }
            }

            attackSpeed += 20 * sortWeapon.Count;
        }
    }
}
