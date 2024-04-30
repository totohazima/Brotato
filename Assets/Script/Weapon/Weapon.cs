using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Only1Games.GDBA;
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
    public string weaponCode;
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

    public string attackType; 
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

    [HideInInspector] public float weaponBasePrice;
    [HideInInspector] public float weaponPrice;
    public string typeText;
    public void StatSetting(int index, int tier)
    {
        WeaponStatInfoTable.Data import = GameManager.instance.gameDataBase.weaponStatInfoTable.table[index];
        
        multipleDamaeCount = import.riseDamageCount;
        multipleDamage = new float[multipleDamaeCount];
        multipleDamageType = new DamageType[multipleDamaeCount];

        weaponNum = import.weaponNum;
        weaponCode = import.weaponCode;
        weaponType = import.weaponType;
        int i = 0;
        switch (tier)
        {
            case 0:
                damage = import.weaponBaseDamage[0];
                bulletCount = import.weaponBaseBulletCount[0];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.riseDamage1[i];
                    multipleDamageType[i] = (DamageType)Enum.Parse(typeof(DamageType), import.riseDamageType[i]);
                    i++;
                }
                criticalChance = import.baseCriticalChance[0];
                criticalDamage = import.baseCriticalDamage[0];
                coolTime = import.baseCoolTime[0];
                knockBack = import.baseKnockback[0];
                range = import.baseRange[0];
                break;
            case 1:
                damage = import.weaponBaseDamage[1];
                bulletCount = import.weaponBaseBulletCount[1];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.riseDamage2[i];
                    multipleDamageType[i] = (DamageType)Enum.Parse(typeof(DamageType), import.riseDamageType[i]);
                    i++;
                }
                criticalChance = import.baseCriticalChance[1];
                criticalDamage = import.baseCriticalDamage[1];
                coolTime = import.baseCoolTime[1];
                knockBack = import.baseKnockback[1];
                range = import.baseRange[1];
                break;
            case 2:
                damage = import.weaponBaseDamage[2];
                bulletCount = import.weaponBaseBulletCount[2];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.riseDamage3[i];
                    multipleDamageType[i] = (DamageType)Enum.Parse(typeof(DamageType), import.riseDamageType[i]);
                    i++;
                }
                criticalChance = import.baseCriticalChance[2];
                criticalDamage = import.baseCriticalDamage[2];
                coolTime = import.baseCoolTime[2];
                knockBack = import.baseKnockback[2];
                range = import.baseRange[2];
                break;
            case 3:
                damage = import.weaponBaseDamage[3];
                bulletCount = import.weaponBaseBulletCount[3];
                while (i < multipleDamaeCount)
                {
                    multipleDamage[i] = import.riseDamage4[i];
                    multipleDamageType[i] = (DamageType)Enum.Parse(typeof(DamageType), import.riseDamageType[i]);
                    i++;
                }
                criticalChance = import.baseCriticalChance[3];
                criticalDamage = import.baseCriticalDamage[3];
                coolTime = import.baseCoolTime[3];
                knockBack = import.baseKnockback[3];
                range = import.baseRange[3];
                break;
        }
        penetrate = import.penetration;
        penetrateDamage = import.penetrationDamage;
        attackType = import.attackType;

        if (attackType == WeaponType.MELEE.ToString())
        {
            typeText = "근거리";
        }
        else if (attackType == WeaponType.RANGE.ToString())
        {
            typeText = "원거리";
        }


    }

    public void AfterStatSetting()
    {
        Player player = StageManager.instance.playerInfo;

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
        if(attackType == WeaponType.MELEE.ToString()) //근접은 스탯 사거리 효과 절반만 받음
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

        //가격 설정
        WeaponBasePriceInfoTable.Data priceImporter = GameManager.instance.gameDataBase.weaponBasePriceInfoTable.table[weaponNum];

        weaponBasePrice = priceImporter.weaponBasePrice[weaponTier];
        int wave = StageManager.instance.waveLevel + 1;
        weaponPrice = (weaponBasePrice + wave + (weaponBasePrice * 0.1f * wave)) * 1;
        weaponPrice = MathF.Round(weaponPrice);
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
