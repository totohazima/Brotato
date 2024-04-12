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

    public enum SettType
    {
        UNARMED,
        TOOL,
        GUN,
        EXPLOSIVE,
        PRECISION,
        NATIVE,
    }
    public int weaponNum;
    public int weaponTier;
    public string name;
    public string weaponType;
    public float damage;
    public int bulletCount;
    public float[] multipleDamage;
    public float criticalChance;
    public float criticalDamage;
    public float coolTime;
    public float knockBack;
    public float range;
    public int penetrate;
    public float penetrateDamage;
    public float bloodSucking;

    public int attackType; //0 = 근거리, 1 = 원거리
    public int multipleDamaeCount; //데미지 계수 갯수
    public DamageType[] multipleDamageType; //데미지 계수들

    [HideInInspector] public float afterDamage;
    [HideInInspector] public int afterBulletCount;
    [HideInInspector] public float afterCriticalChance;
    [HideInInspector] public float afterCriticalDamage;
    [HideInInspector] public float afterCoolTime;
    [HideInInspector] public float afterRange;
    [HideInInspector] public int afterPenetrate;
    [HideInInspector] public float afterPenetrateDamage;
    [HideInInspector] public float afterBloodSucking;
    [HideInInspector] public float afterKnockBack;

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
                bulletCount = import.bulletCount1[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage1[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType),import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance1[index];
                criticalDamage = import.criticalDamage1[index];
                coolTime = import.coolTIme1[index];
                knockBack = import.knockBack1[index];
                range = import.range1[index];
                break;
            case 1:
                damage = import.damage2[index];
                bulletCount = import.bulletCount2[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage2[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType), import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance2[index];
                criticalDamage = import.criticalDamage2[index];
                coolTime = import.coolTIme2[index];
                knockBack = import.knockBack2[index];
                range = import.range2[index];
                break;
            case 2:
                damage = import.damage3[index];
                bulletCount = import.bulletCount3[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage3[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType), import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance3[index];
                criticalDamage = import.criticalDamage3[index];
                coolTime = import.coolTIme3[index];
                knockBack = import.knockBack3[index];
                range = import.range3[index];
                break;
            case 3:
                damage = import.damage4[index];
                bulletCount = import.bulletCount4[index];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.multipleDamage4[index + i];
                    multipleDamageType[i] = (DamageType)System.Enum.Parse(typeof(DamageType), import.multipleDamageType[index + i]);
                    i++;
                }
                criticalChance = import.criticalChance4[index];
                criticalDamage = import.criticalDamage4[index];
                coolTime = import.coolTIme4[index];
                knockBack = import.knockBack4[index];
                range = import.range4[index];
                break;
        }
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

    public void AfterStatSetting()
    {
        Player player = GameManager.instance.playerInfo;

        afterDamage = damage;
        for (int i = 0; i < multipleDamaeCount; i++)
        {
            switch (multipleDamageType[i])
            {
                case DamageType.MELEE:
                    afterDamage += player.meleeDamage * (multipleDamage[i] / 100);
                    break;
                case DamageType.RANGE:
                    afterDamage += player.rangeDamage * (multipleDamage[i] / 100);
                    break;
                case DamageType.HEALTH:
                    afterDamage += player.maxHealth * (multipleDamage[i] / 100);
                    break;
                case DamageType.ENGINE:
                    afterDamage += player.engine * (multipleDamage[i] / 100);
                    break;
            }
        }
        afterDamage *= 1 + (player.persentDamage / 100);

        afterBulletCount = bulletCount;
        afterPenetrate = penetrate + player.penetrate;
        afterCriticalChance = criticalChance + player.criticalChance;
        afterCriticalDamage = criticalDamage;
        afterCoolTime = coolTime - ((coolTime / 100) * player.attackSpeed);
        if(afterCoolTime < 0.05f) //최소 공속 0.05초
        {
            afterCoolTime = 0.05f;
        }
        float preRange;
        if(attackType == (int)WeaponType.MELEE) //근접은 스탯 사거리 효과 절반만 받음
        {
            preRange = range + player.range / 2;
        }
        else
        {
            preRange = range + player.range;
        }
        afterRange = preRange / 10;
        afterPenetrateDamage = -penetrateDamage + player.penetrateDamage;
        afterBloodSucking = bloodSucking + player.bloodSucking;
        afterKnockBack = knockBack + player.KnockBack;
    }
    public float GetAngle(Vector2 start, Vector2 end)//각도구하기
    {
        Vector2 vectorPos = end - start;
        return Mathf.Atan2(vectorPos.y, vectorPos.x) * Mathf.Rad2Deg;
    }

    public Vector3 ConvertAngleToVector(float _deg, float width)//각도로 좌표 구하기
    {
        var rad = _deg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * width, Mathf.Sin(rad) * width, 2);
    }
}
