using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseStatImporter : MonoBehaviour
{
    public static EnemyBaseStatImporter instance;

    [HideInInspector] public string[] enemyName;
    [HideInInspector] public float[] health;
    [HideInInspector] public float[] damage;
    [HideInInspector] public float[] coolTime;
    [HideInInspector] public float[] armor;
    [HideInInspector] public float[] range;
    [HideInInspector] public float[] evasion;
    [HideInInspector] public float[] accuracy;
    [HideInInspector] public float[] minSpeed;
    [HideInInspector] public float[] maxSpeed;
    [HideInInspector] public float[] moneyDropNum;
    [HideInInspector] public int[] moneyValue;
    [HideInInspector] public int[] expValue;
    [HideInInspector] public float[] consumDropRate;
    [HideInInspector] public float[] LootDropRate;

    void Awake()
    {
        instance = this;
        string filePath = "Assets/Resources/CSV.data/StatInfo/EnemyBaseStat.xlsx";
        List<Dictionary<int, object>> data = ExcelReader.ReadNumericColumns(filePath);

        enemyName = new string[data.Count];
        health = new float[data.Count];
        damage = new float[data.Count];
        coolTime = new float[data.Count];
        armor = new float[data.Count];
        range = new float[data.Count];
        evasion = new float[data.Count];
        accuracy = new float[data.Count];
        minSpeed = new float[data.Count];
        maxSpeed = new float[data.Count];
        moneyDropNum = new float[data.Count];
        moneyValue = new int[data.Count];
        expValue = new int[data.Count];
        consumDropRate = new float[data.Count];
        LootDropRate = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        { 
            enemyName[i] = (string)data[i][0];

            // 각 열의 데이터를 적절한 형식으로 변환하여 저장
            if (float.TryParse(data[i][1].ToString(), out float healthValue))
            {
                health[i] = healthValue;
            }

            if (float.TryParse(data[i][2].ToString(), out float damageValue))
            {
                damage[i] = damageValue;
            }

            if (float.TryParse(data[i][3].ToString(), out float coolTimeValue))
            {
                coolTime[i] = coolTimeValue;
            }

            if (float.TryParse(data[i][4].ToString(), out float armorValue))
            {
                armor[i] = armorValue;
            }

            if (float.TryParse(data[i][5].ToString(), out float rangeValue))
            {
                range[i] = rangeValue;
            }

            if (float.TryParse(data[i][6].ToString(), out float evasionValue))
            {
                evasion[i] = evasionValue;
            }

            if (float.TryParse(data[i][7].ToString(), out float accuracyValue))
            {
                accuracy[i] = accuracyValue;
            }

            if (float.TryParse(data[i][8].ToString(), out float minSpeedValue))
            {
                minSpeed[i] = minSpeedValue;
            }

            if (float.TryParse(data[i][9].ToString(), out float maxSpeedValue))
            {
                maxSpeed[i] = maxSpeedValue;
            }

            if (float.TryParse(data[i][10].ToString(), out float moneyDropNumValue))
            {
                moneyDropNum[i] = moneyDropNumValue;
            }

            if (int.TryParse(data[i][11].ToString(), out int moneyValueValue))
            {
                moneyValue[i] = moneyValueValue;
            }

            if (int.TryParse(data[i][12].ToString(), out int expValueValue))
            {
                expValue[i] = expValueValue;
            }

            if (float.TryParse(data[i][13].ToString(), out float consumDropRateValue))
            {
                consumDropRate[i] = consumDropRateValue;
            }

            if (float.TryParse(data[i][14].ToString(), out float LootDropRateValue))
            {
                LootDropRate[i] = LootDropRateValue;
            }
        }
    }
}
