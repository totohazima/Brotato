using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
        string filePath;
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath + "WaveStat.xlsx");
#elif UNITY_IOS && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath+ "WaveStat.xlsx");
#else
        filePath = "Assets/Resources/CSV.data/StatInfo/WaveStat.xlsx";
#endif

        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("WaveStat");
        enemyCount = new int[data.Count];
        enemySpawnCount = new int[data.Count];
        waveTime = new float[data.Count];
        treeCount = new float[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            if (int.TryParse(data[i][0].ToString(), out int enemyCountValue))
            {
                enemyCount[i] = enemyCountValue;
            }
            if (int.TryParse(data[i][1].ToString(), out int enemySpawnCountValue))
            {
                enemySpawnCount[i] = enemySpawnCountValue;
            }
            if (float.TryParse(data[i][2].ToString(), out float waveTimeValue))
            {
                waveTime[i] = waveTimeValue;
            }
            if (float.TryParse(data[i][3].ToString(), out float treeCountValue))
            {
                treeCount[i] = treeCountValue;
            }
        }
    }
}
