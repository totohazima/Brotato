using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatImporter : MonoBehaviour
{
    public static PlayerStatImporter instance;

    public int[] characterNum;
    public string[] characterName;
    public float[] maxHealth;
    public float[] regeneration;
    public float[] bloodSucking;
    public float[] persentDamage;
    public float[] meleeDamage;
    public float[] rangeDamage;
    public float[] attackSpeed;
    public float[] criticalChance;
    public float[] engine;
    public float[] range;
    public float[] armor;
    public float[] evasion;
    public float[] accuracy;
    public float[] speed;

    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("PlayerStat");

        characterNum = new int[data.Count];
        characterName = new string[data.Count];
        maxHealth = new float[data.Count];
        regeneration = new float[data.Count];
        bloodSucking = new float[data.Count];
        persentDamage = new float[data.Count];
        meleeDamage = new float[data.Count];
        rangeDamage = new float[data.Count];
        attackSpeed = new float[data.Count];
        criticalChance = new float[data.Count];
        engine = new float[data.Count];
        range = new float[data.Count];
        armor = new float[data.Count];
        evasion = new float[data.Count];
        accuracy = new float[data.Count];
        speed = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            characterNum[i] = (int)data[i]["PlayerNum"];
            characterName[i] = (string)data[i]["Name"];

            int j = (int)data[i]["Health"];
            maxHealth[i] = j;
            j = (int)data[i]["Regeneration"];
            regeneration[i] = j;
            j = (int)data[i]["BloodSucking"];
            bloodSucking[i] = j;
            j = (int)data[i]["PersentDamage"];
            persentDamage[i] = j;
            j = (int)data[i]["MeleeDamage"];
            meleeDamage[i] = j;
            j = (int)data[i]["RangeDamage"];
            rangeDamage[i] = j;
            j = (int)data[i]["AttackSpeed"];
            attackSpeed[i] = j;
            j = (int)data[i]["CriticalChance"];
            criticalChance[i] = j;
            j = (int)data[i]["Engine"];
            engine[i] = j;
            j = (int)data[i]["Range"];
            range[i] = j;
            j = (int)data[i]["Armor"];
            armor[i] = j;
            j = (int)data[i]["Evasion"];
            evasion[i] = j;
            j = (int)data[i]["Accuracy"];
            accuracy[i] = j;
            j = (int)data[i]["Speed"];
            speed[i] = j;
        }
    }
}
