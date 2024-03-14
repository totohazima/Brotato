using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatImporter : MonoBehaviour
{
    public static WeaponStatImporter instance;

    [HideInInspector] public int[] weaponNum;
    [HideInInspector] public string[] name;
    [HideInInspector] public float[] damage1, damage2, damage3, damage4;
    [HideInInspector] public int[] multipleDamageCount;
    [HideInInspector] public string[] multipleDamageType;
    [HideInInspector] public float[] multipleDamage1, multipleDamage2, multipleDamage3, multipleDamage4;
    [HideInInspector] public float[] criticalChance1, criticalChance2, criticalChance3, criticalChance4;
    [HideInInspector] public float[] criticalDamage1, criticalDamage2, criticalDamage3, criticalDamage4;
    [HideInInspector] public float[] coolTIme1, coolTIme2, coolTIme3, coolTIme4;
    [HideInInspector] public float[] knockBack1, knockBack2, knockBack3, knockBack4;
    [HideInInspector] public float[] range1, range2, range3, range4;
    [HideInInspector] public int[] penetrate;
    [HideInInspector] public float[] penetrateDamage;
    [HideInInspector] public int[] type;
    [HideInInspector] public string[] weaponType;

    void Awake()
    {
        instance = this;
        List<Dictionary<string, object>> data = CSVReaderStat.Read("WeaponStat");
        weaponNum = new int[data.Count];
        name = new string[data.Count];

        damage1 = new float[data.Count];
        damage2 = new float[data.Count];
        damage3 = new float[data.Count];
        damage4 = new float[data.Count];

        multipleDamageCount = new int[data.Count];
        multipleDamageType = new string[data.Count];

        multipleDamage1 = new float[data.Count];
        multipleDamage2 = new float[data.Count];
        multipleDamage3 = new float[data.Count];
        multipleDamage4 = new float[data.Count];

        criticalChance1 = new float[data.Count];
        criticalChance2 = new float[data.Count];
        criticalChance3 = new float[data.Count];
        criticalChance4 = new float[data.Count];

        criticalDamage1 = new float[data.Count];
        criticalDamage2 = new float[data.Count];
        criticalDamage3 = new float[data.Count];
        criticalDamage4 = new float[data.Count];

        coolTIme1 = new float[data.Count];
        coolTIme2 = new float[data.Count];
        coolTIme3 = new float[data.Count];
        coolTIme4 = new float[data.Count];

        knockBack1 = new float[data.Count];
        knockBack2 = new float[data.Count];
        knockBack3 = new float[data.Count];
        knockBack4 = new float[data.Count];

        range1 = new float[data.Count];
        range2 = new float[data.Count];
        range3 = new float[data.Count];
        range4 = new float[data.Count];

        penetrate = new int[data.Count];
        penetrateDamage = new float[data.Count];
        type = new int[data.Count];
        weaponType = new string[data.Count];

        for(int i = 0; i < data.Count; i++)
        {
            weaponNum[i] = (int)data[i]["WeaponNum"];
            name[i] = (string)data[i]["Name"];

            int j = (int)data[i]["Damage1"];
            damage1[i] = j;
            j = (int)data[i]["Damage2"];
            damage2[i] = j;
            j = (int)data[i]["Damage3"];
            damage3[i] = j;
            j = (int)data[i]["Damage4"];
            damage4[i] = j;

            multipleDamageCount[i] = (int)data[i]["MultipleDamageCount"];
            multipleDamageType[i] = (string)data[i]["MultipleDamageType"];

            j = (int)data[i]["MultipleDamage1"];
            multipleDamage1[i] = j;
            j = (int)data[i]["MultipleDamage2"];
            multipleDamage2[i] = j;
            j = (int)data[i]["MultipleDamage3"];
            multipleDamage3[i] = j;
            j = (int)data[i]["MultipleDamage4"];
            multipleDamage4[i] = j;

            criticalChance1[i] = (float)data[i]["CriticalChance1"];
            criticalChance2[i] = (float)data[i]["CriticalChance2"];
            criticalChance3[i] = (float)data[i]["CriticalChance3"];
            criticalChance4[i] = (float)data[i]["CriticalChance4"];

            criticalDamage1[i] = (float)data[i]["CriticalDamage1"];
            criticalDamage2[i] = (float)data[i]["CriticalDamage2"];
            criticalDamage3[i] = (float)data[i]["CriticalDamage3"];
            criticalDamage4[i] = (float)data[i]["CriticalDamage4"];

            coolTIme1[i] = (float)data[i]["CoolTime1"];
            coolTIme2[i] = (float)data[i]["CoolTime2"];
            coolTIme3[i] = (float)data[i]["CoolTime3"];
            coolTIme4[i] = (float)data[i]["CoolTime4"];

            knockBack1[i] = (int)data[i]["KnockBack1"];
            knockBack2[i] = (int)data[i]["KnockBack2"];
            knockBack3[i] = (int)data[i]["KnockBack3"];
            knockBack4[i] = (int)data[i]["KnockBack4"];

            range1[i] = (int)data[i]["Range1"];
            range2[i] = (int)data[i]["Range2"];
            range3[i] = (int)data[i]["Range3"];
            range4[i] = (int)data[i]["Range4"];

            penetrate[i] = (int)data[i]["Penetrate"];
            j = (int)data[i]["PenetrateDamage"];
            penetrateDamage[i] = j;
            type[i] = (int)data[i]["Type"];
            weaponType[i] = (string)data[i]["WeaponType"];
        }
    }
}
