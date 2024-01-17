using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Weapons
    {
        PISTOL,
        SHOTGUN,
        DOUBLESHOTGUN,
        SPEAR,
        SHREDDER,
        PUNCH,
        RENCH,
        DRIVER
    }

    public enum Type
    {
        MELEE,
        RANGE,
        ENGINE,
    }
    public int weaponNum;

    public string name;
    public float damage;
    public float criticalChance;
    public float coolTIme;
    public float knockBack;
    public float range;
    public int penetrate;
    public int weaponType; //0 = 근거리, 1 = 원거리, 2 = 엔진(포탑류)

    public string typeText;
    public void StatSetting(int index)
    {
        WeaponStatImporter import = WeaponStatImporter.instance;

        weaponNum = import.weaponNum[index];
        name = import.name[index];
        damage = import.damage[index];
        criticalChance = import.criticalChance[index];
        coolTIme = import.coolTIme[index];
        knockBack = import.knockBack[index];
        range = import.range[index];
        penetrate = import.penetrate[index];
        weaponType = import.type[index];
        
        if(weaponType == (int)Type.MELEE)
        {
            typeText = "근거리";
        }
        else if (weaponType == (int)Type.RANGE)
        {
            typeText = "원거리";
        }
        else if (weaponType == (int)Type.ENGINE)
        {
            typeText = "엔진";
        }
    }
}
