using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DifficultImporter : MonoBehaviour
{
    public static DifficultImporter instance;

    [HideInInspector] public bool[] isSpecialEnemy;//새로운 적의 출현
    [HideInInspector] public bool[] isEliteSpawn; //엘리트와 무리가 등장
    [HideInInspector] public int[] isEliteWaveCount; //엘리트와 무리가 등장하는 웨이브 수(6라운드 부터 등장)
    [HideInInspector] public float[] enemyRiseDamage; //적 데미지 증가치 %
    [HideInInspector] public float[] enemyRiseHealth; //적 체력 증가치 %
    [HideInInspector] public bool[] doubleBoss; //보스가 2마리 (체력은 25% 감소)
    void Awake()
    {
        instance = this;
        string filePath;
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, "DifficultStat.xlsx");
#elif UNITY_IOS && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, "DifficultStat.xlsx");
#else
        filePath = "Assets/Resources/CSV.data/StatInfo/DifficultStat.xlsx";
#endif
        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("DifficultStat");

        isSpecialEnemy = new bool[data.Count];
        isEliteSpawn = new bool[data.Count];
        isEliteWaveCount = new int[data.Count];
        enemyRiseDamage = new float[data.Count];
        enemyRiseHealth = new float[data.Count];
        doubleBoss = new bool[data.Count];

        for(int i = 0; i < data.Count; i++)
        {
            if (bool.TryParse(data[i][1].ToString(), out bool specialValue))
            {
                isSpecialEnemy[i] = specialValue;
            }
            if (bool.TryParse(data[i][2].ToString(), out bool eliteValue))
            {
                isEliteSpawn[i] = eliteValue;
            }
            if (int.TryParse(data[i][3].ToString(), out int countValue))
            {
                isEliteWaveCount[i] = countValue;
            }
            if (float.TryParse(data[i][4].ToString(), out float riseDamageValue))
            {
                enemyRiseDamage[i] = riseDamageValue;
            }
            if (float.TryParse(data[i][5].ToString(), out float riseHealthValue))
            {
                enemyRiseHealth[i] = riseHealthValue;
            }
            if (bool.TryParse(data[i][6].ToString(), out bool doubleBossValue))
            {
                doubleBoss[i] = doubleBossValue;
            }
        }
    }


}
