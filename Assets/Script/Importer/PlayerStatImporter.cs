using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatImporter : MonoBehaviour
{
    public static PlayerStatImporter instance;

    [HideInInspector] public int[] characterNum;
    [HideInInspector] public string[] characterName;
    [HideInInspector] public float[] maxHealth;
    [HideInInspector] public float[] regeneration;
    [HideInInspector] public float[] bloodSucking;
    [HideInInspector] public float[] persentDamage;
    [HideInInspector] public float[] meleeDamage;
    [HideInInspector] public float[] rangeDamage;
    [HideInInspector] public float[] attackSpeed;
    [HideInInspector] public float[] criticalChance;
    [HideInInspector] public float[] engine;
    [HideInInspector] public float[] range;
    [HideInInspector] public float[] armor;
    [HideInInspector] public float[] evasion;
    [HideInInspector] public float[] accuracy;
    [HideInInspector] public float[] speed;

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

            if (float.TryParse(data[i]["Health"].ToString(), out float healthValue))
            {
                maxHealth[i] = healthValue;
            }

            if (float.TryParse(data[i]["Regeneration"].ToString(), out float regenerationValue))
            {
                regeneration[i] = regenerationValue;
            }

            if (float.TryParse(data[i]["BloodSucking"].ToString(), out float bloodSuckingValue))
            {
                bloodSucking[i] = bloodSuckingValue;
            }

            if (float.TryParse(data[i]["PersentDamage"].ToString(), out float persentDamageValue))
            {
                persentDamage[i] = persentDamageValue;
            }

            if (float.TryParse(data[i]["MeleeDamage"].ToString(), out float meleeDamageValue))
            {
                meleeDamage[i] = meleeDamageValue;
            }

            if (float.TryParse(data[i]["RangeDamage"].ToString(), out float rangeDamageValue))
            {
                rangeDamage[i] = rangeDamageValue;
            }

            if (float.TryParse(data[i]["AttackSpeed"].ToString(), out float attackSpeedValue))
            {
                attackSpeed[i] = attackSpeedValue;
            }

            if (float.TryParse(data[i]["CriticalChance"].ToString(), out float criticalChanceValue))
            {
                criticalChance[i] = criticalChanceValue;
            }

            if (float.TryParse(data[i]["Engine"].ToString(), out float engineValue))
            {
                engine[i] = engineValue;
            }

            if (float.TryParse(data[i]["Range"].ToString(), out float rangeValue))
            {
                range[i] = rangeValue;
            }

            if (float.TryParse(data[i]["Armor"].ToString(), out float armorValue))
            {
                armor[i] = armorValue;
            }

            if (float.TryParse(data[i]["Evasion"].ToString(), out float evasionValue))
            {
                evasion[i] = evasionValue;
            }

            if (float.TryParse(data[i]["Accuracy"].ToString(), out float accuracyValue))
            {
                accuracy[i] = accuracyValue;
            }

            if (float.TryParse(data[i]["Speed"].ToString(), out float speedValue))
            {
                speed[i] = speedValue;
            }
        }
    }
}
