using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStatImporter : MonoBehaviour
{
    public static WeaponStatImporter instance;

    public int[] weaponNum;
    public string[] name;
    public float[] damage;
    public float[] criticalChance;
    public float[] coolTIme;
    public float[] knockBack;
    public float[] range;
    public int[] penetrate;
    public int[] type;

    void Awake()
    {
        instance = this;
        List<Dictionary<string, object>> data = CSVReaderStat.Read("WeaponStat");
        weaponNum = new int[data.Count];
        name = new string[data.Count];
        damage = new float[data.Count];
        criticalChance = new float[data.Count];
        coolTIme = new float[data.Count];
        knockBack = new float[data.Count];
        range = new float[data.Count];
        penetrate = new int[data.Count];
        type = new int[data.Count];

        for(int i = 0; i < data.Count; i++)
        {
            weaponNum[i] = (int)data[i]["WeaponNum"];
            name[i] = (string)data[i]["Name"];

            int j = (int)data[i]["Damage"];
            damage[i] = j;
            j = (int)data[i]["Critical"];
            criticalChance[i] = j;
            j = (int)data[i]["CoolTime"];
            coolTIme[i] = j;
            j = (int)data[i]["KnockBack"];
            knockBack[i] = j;
            j = (int)data[i]["Range"];
            range[i] = j;

            penetrate[i] = (int)data[i]["Penetrate"];
            type[i] = (int)data[i]["Type"];
        }
    }
}
