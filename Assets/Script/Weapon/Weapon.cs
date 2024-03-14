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
    public float coolTime;
    public float knockBack;
    public float range;
    public int penetrate;
    public float penetrateDamage;
    public float bloodSucking;

    public int attackType; //0 = �ٰŸ�, 1 = ���Ÿ�
    public int multipleDamaeCount; //������ ��� ����
    public DamageType[] multipleDamageType; //������ �����

    public float afterDamage;
    public float afterCriticalChance;
    public float afterCriticalDamage;
    public float afterCoolTime;
    public float afterRange;
    public int afterPenetrate;
    public float afterPenetrateDamage;
    public float afterBloodSucking;
    public float afterKnockBack;

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
                coolTime = import.coolTIme1[index];
                knockBack = import.knockBack1[index];
                range = import.range1[index];
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
                coolTime = import.coolTIme2[index];
                knockBack = import.knockBack2[index];
                range = import.range2[index];
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
                coolTime = import.coolTIme3[index];
                knockBack = import.knockBack3[index];
                range = import.range3[index];
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
            typeText = "�ٰŸ�";
        }
        else if (attackType == (int)WeaponType.RANGE)
        {
            typeText = "���Ÿ�";
        }


    }

    public void BulletSetting()
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

        afterPenetrate = penetrate + player.penetrate;
        afterCriticalChance = criticalChance + player.criticalChance;
        afterCriticalDamage = criticalDamage;
        afterCoolTime = coolTime - ((coolTime / 100) * player.attackSpeed);
        if(afterCoolTime < 0.05f) //�ּ� ���� 0.05��
        {
            afterCoolTime = 0.05f;
        }
        if(attackType == (int)WeaponType.MELEE)
        {
            afterRange = range + player.range / 2;
        }
        else
        {
            afterRange = range + player.range;
        }
        afterPenetrateDamage = -penetrateDamage + player.penetrateDamage;
        afterBloodSucking = bloodSucking + player.bloodSucking;
        afterKnockBack = knockBack + player.KnockBack;
    }
}
