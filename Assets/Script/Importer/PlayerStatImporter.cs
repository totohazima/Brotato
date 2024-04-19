using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
    public float[] speed;

    void Awake()
    {
        instance = this;
        string filePath;
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, "PlayerStat.xlsx");
#elif UNITY_IOS && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, "PlayerStat.xlsx");
#else
        filePath = "Assets/Resources/CSV.data/StatInfo/PlayerStat.xlsx";
#endif
        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("PlayerStat");

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
            characterNum[i] = (int)data[i][0];
            characterName[i] = (string)data[i][1];

            if (float.TryParse(data[i][2].ToString(), out float healthValue))
            {
                maxHealth[i] = healthValue;
            }

            if (float.TryParse(data[i][3].ToString(), out float regenerationValue))
            {
                regeneration[i] = regenerationValue;
            }

            if (float.TryParse(data[i][4].ToString(), out float bloodSuckingValue))
            {
                bloodSucking[i] = bloodSuckingValue;
            }

            if (float.TryParse(data[i][5].ToString(), out float persentDamageValue))
            {
                persentDamage[i] = persentDamageValue;
            }

            if (float.TryParse(data[i][6].ToString(), out float meleeDamageValue))
            {
                meleeDamage[i] = meleeDamageValue;
            }

            if (float.TryParse(data[i][7].ToString(), out float rangeDamageValue))
            {
                rangeDamage[i] = rangeDamageValue;
            }

            if (float.TryParse(data[i][8].ToString(), out float attackSpeedValue))
            {
                attackSpeed[i] = attackSpeedValue;
            }

            if (float.TryParse(data[i][9].ToString(), out float criticalChanceValue))
            {
                criticalChance[i] = criticalChanceValue;
            }

            if (float.TryParse(data[i][10].ToString(), out float engineValue))
            {
                engine[i] = engineValue;
            }

            if (float.TryParse(data[i][11].ToString(), out float rangeValue))
            {
                range[i] = rangeValue;
            }

            if (float.TryParse(data[i][12].ToString(), out float armorValue))
            {
                armor[i] = armorValue;
            }

            if (float.TryParse(data[i][13].ToString(), out float evasionValue))
            {
                evasion[i] = evasionValue;
            }

            if (float.TryParse(data[i][14].ToString(), out float accuracyValue))
            {
                accuracy[i] = accuracyValue;
            }

            if (float.TryParse(data[i][15].ToString(), out float speedValue))
            {
                speed[i] = speedValue;
            }
        }
    }
}
