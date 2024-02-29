using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatImporter : MonoBehaviour
{
    public static WeaponStatImporter instance;

    [HideInInspector] public int[] weaponNum;
    [HideInInspector] public string[] name;

    [HideInInspector] public float[] damage1;
    public float[] damage2;
    public float[] damage3;
    public float[] damage4;

    public int[] multipleDamageCount;
    public string[] multipleDamageType;

    public float[] multipleDamage1;
    public float[] multipleDamage2;
    public float[] multipleDamage3;
    public float[] multipleDamage4;

    [HideInInspector] public float[] criticalChance1;
    public float[] criticalChance2;
    public float[] criticalChance3;
    public float[] criticalChance4;

    public float[] criticalDamage1;
    public float[] criticalDamage2;
    public float[] criticalDamage3;
    public float[] criticalDamage4;
    [HideInInspector] public float[] coolTIme;
    [HideInInspector] public float[] knockBack;
    [HideInInspector] public float[] range;
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

        coolTIme = new float[data.Count];
        knockBack = new float[data.Count];
        range = new float[data.Count];
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
            

            j = (int)data[i]["CoolTime"];
            coolTIme[i] = j;
            j = (int)data[i]["KnockBack"];
            knockBack[i] = j;
            j = (int)data[i]["Range"];
            range[i] = j;

            penetrate[i] = (int)data[i]["Penetrate"];
            j = (int)data[i]["PenetrateDamage"];
            penetrateDamage[i] = j;
            type[i] = (int)data[i]["Type"];
            weaponType[i] = (string)data[i]["WeaponType"];
        }
    }
}
