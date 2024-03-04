using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Weapons
    {
        PISTOL,
        DOUBLESHOTGUN,
        SPEAR,
        SHREDDER,
        PUNCH,
        WRENCH,
        DRIVER
    }

    public enum WeaponType
    {
        MELEE,
        RANGE,
    }

    public enum DamageType
    {
        MELEE,
        RANGE,
        HEALTH,
        ENGINE,
    }
    public int weaponNum;
    public int weaponTier;
    public string name;
    public string weaponType;
    public float damage;
    public float[] multipleDamage;
    public float criticalChance;
    public float criticalDamage;
    public float coolTIme;
    public float knockBack;
    public float afterKnockBack;
    public float range;
    public int penetrate;
    public float penetrateDamage;
    public float bloodSucking;

    public int attackType; //0 = 근거리, 1 = 원거리
    public int multipleDamaeCount; //데미지 계수 종류
    public DamageType[] multipleDamageType; //데미지 계수들

    public float afterDamage;
    public float afterCriticalChance;
    public float afterCriticalDamage;
    public float afterCoolTime;
    public float afterRange;
    public int afterPenetrate;
    public float afterPenetrateDamage;
    public float afterBloodSucking;

    public string typeText;
    public void StatSetting(int index, int tier)
    {
        WeaponStatImporter import = WeaponStatImporter.instance;

        multipleDamaeCount = import.multipleDamageCount[index];
        multipleDamage = new float[multipleDamaeCount];
        multipleDamageType = new DamageType[multipleDamaeCount];

        weaponNum = import.weaponNum[index];
        name = import.name[index];
        weaponType = import.weaponType[index];
        int i = 0;
        switch (tier)
        {
            case 0:
                damage = import.damage1[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage1[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType),import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance1[index];
                criticalDamage = import.criticalDamage1[index];
                break;
            case 1:
                damage = import.damage2[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage2[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType), import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance2[index];
                criticalDamage = import.criticalDamage2[index];
                break;
            case 2:
                damage = import.damage3[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage3[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType), import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance3[index];
                criticalDamage = import.criticalDamage3[index];
                break;
            case 3:
                damage = import.damage4[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage4[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType), import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance4[index];
                criticalDamage = import.criticalDamage4[index];
                break;
        }
        coolTIme = import.coolTIme[index];
        knockBack = import.knockBack[index];
        range = import.range[index];
        penetrate = import.penetrate[index];
        penetrateDamage = import.penetrateDamage[index];
        attackType = import.type[index];
        
        if(attackType == (int)WeaponType.MELEE)
        {
            typeText = "근거리";
        }
        else if (attackType == (int)WeaponType.RANGE)
        {
            typeText = "원거리";
        }


    }
}
