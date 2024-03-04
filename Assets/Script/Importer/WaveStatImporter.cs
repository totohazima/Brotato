using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveStatImporter : MonoBehaviour
{
    public static WaveStatImporter instance;

    public int[] enemyCount;
    public int[] enemySpawnCount;
    public float[] waveTime;

    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("WaveStat");
        enemyCount = new int[data.Count];
        enemySpawnCount = new int[data.Count];
        waveTime = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            enemyCount[i] = (int)data[i]["EnemyCount"];
            enemySpawnCount[i] = (int)data[i]["EnemySpawnCount"];
            int j = (int)data[i]["WaveTime"];
            waveTime[i] = j;
        }
    }
}
