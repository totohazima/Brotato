using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStatImporter : MonoBehaviour
{
    public static WaveStatImporter instance;

    public int[] enemyCount;
    public int[] enemySpawnCount;
    public float[] waveTime;
    public int[] treeCount;
    public Wave_Scriptable[] wave_Scriptables;
    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("WaveStat");
        enemyCount = new int[data.Count];
        enemySpawnCount = new int[data.Count];
        waveTime = new float[data.Count];
        treeCount = new int[data.Count];
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
            if (int.TryParse(data[i]["TreeCount"].ToString(), out int treeCountValue))
            {
                treeCount[i] = treeCountValue;
            }
        }
    }
}
