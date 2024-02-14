using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatImporter : MonoBehaviour
{
    public static EnemyStatImporter instance;

    [HideInInspector] public string[] enemyName;
    [HideInInspector] public float[] health;
    [HideInInspector] public float[] healthWave;
    [HideInInspector] public float[] damage;
    [HideInInspector] public float[] damageWave;
    [HideInInspector] public float[] coolTime;
    [HideInInspector] public float[] armor;
    [HideInInspector] public float[] range;
    [HideInInspector] public float[] evasion;
    [HideInInspector] public float[] accuracy;
    [HideInInspector] public float[] minSpeed;
    [HideInInspector] public float[] maxSpeed;
    [HideInInspector] public float[] moneyDropNum;
    [HideInInspector] public float[] consumDropRate;
    [HideInInspector] public float[] LootDropRate;

    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("EnemyStat");

        enemyName = new string[data.Count];
        health = new float[data.Count];
        healthWave = new float[data.Count];
        damage = new float[data.Count];
        damageWave = new float[data.Count];
        coolTime = new float[data.Count];
        armor = new float[data.Count];
        range = new float[data.Count];
        evasion = new float[data.Count];
        accuracy = new float[data.Count];
        minSpeed = new float[data.Count];
        maxSpeed = new float[data.Count];
        moneyDropNum = new float[data.Count];
        consumDropRate = new float[data.Count];
        LootDropRate = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            enemyName[i] = (string)data[i]["EnemyName"];

            int j = (int)data[i]["Health"];
            health[i] = j;
            healthWave[i] = (float)data[i]["HealthWave"];
            j = (int)data[i]["Damage"];
            damage[i] = j;
            damageWave[i] = (float)data[i]["DamageWave"];
            j = (int)data[i]["CoolTime"];
            coolTime[i] = j;
            j = (int)data[i]["Armor"];
            armor[i] = j;
            j = (int)data[i]["Range"];
            range[i] = j;
            j = (int)data[i]["Evasion"];
            evasion[i] = j;
            j = (int)data[i]["Accuracy"];
            accuracy[i] = j;
            j = (int)data[i]["MinSpeed"];
            minSpeed[i] = j;
            j = (int)data[i]["MaxSpeed"];
            maxSpeed[i] = j;
            j = (int)data[i]["MoneyDrop"];
            moneyDropNum[i] = j;
            j = (int)data[i]["ConsumDrop"];
            consumDropRate[i] = j;
            j = (int)data[i]["LootDrop"];
            LootDropRate[i] = j;
        }
    }
}
