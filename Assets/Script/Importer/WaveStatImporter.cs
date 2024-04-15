using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStatImporter : MonoBehaviour
{
    public static WaveStatImporter instance;

    [HideInInspector] public int[] enemyCount;
    [HideInInspector] public int[] enemySpawnCount;
    [HideInInspector] public float[] waveTime;
    [HideInInspector] public float[] treeCount;
    public Wave_Scriptable[] wave_Scriptables;
    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("WaveStat");
        enemyCount = new int[data.Count];
        enemySpawnCount = new int[data.Count];
        waveTime = new float[data.Count];
        treeCount = new float[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            if (int.TryParse(data[i]["EnemyCount"].ToString(), out int enemyCountValue))
            {
                enemyCount[i] = enemyCountValue;
            }
            if (int.TryParse(data[i]["EnemySpawnCount"].ToString(), out int enemySpawnCountValue))
            {
                enemySpawnCount[i] = enemySpawnCountValue;
            }
            if (float.TryParse(data[i]["WaveTime"].ToString(), out float waveTimeValue))
            {
                waveTime[i] = waveTimeValue;
            }
            if (float.TryParse(data[i]["TreeCount"].ToString(), out float treeCountValue))
            {
                treeCount[i] = treeCountValue;
            }
        }
    }
}
