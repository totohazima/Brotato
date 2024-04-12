using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultImporter : MonoBehaviour
{
    public static DifficultImporter instance;

    public bool[] isSpecialEnemy;//새로운 적의 출현
    public bool[] isEliteSpawn; //엘리트와 무리가 등장
    public int[] isEliteWaveCount; //엘리트와 무리가 등장하는 웨이브 수(6라운드 부터 등장)
    public float[] enemyRiseDamage; //적 데미지 증가치 %
    public float[] enemyRiseHealth; //적 체력 증가치 %
    public bool[] doubleBoss; //보스가 2마리 (체력은 25% 감소)
    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("DifficultStat");

        isSpecialEnemy = new bool[data.Count];
        isEliteSpawn = new bool[data.Count];
        isEliteWaveCount = new int[data.Count];
        enemyRiseDamage = new float[data.Count];
        enemyRiseHealth = new float[data.Count];
        doubleBoss = new bool[data.Count];

        for(int i = 0; i < data.Count; i++)
        {
            if (bool.TryParse(data[i]["SpecialEnemy"].ToString(), out bool specialValue))
            {
                isSpecialEnemy[i] = specialValue;
            }
            if (bool.TryParse(data[i]["EliteEnemy"].ToString(), out bool eliteValue))
            {
                isEliteSpawn[i] = eliteValue;
            }
            if (int.TryParse(data[i]["EliteWaveCount"].ToString(), out int countValue))
            {
                isEliteWaveCount[i] = countValue;
            }
            if (float.TryParse(data[i]["EnemyRiseDM"].ToString(), out float riseDamageValue))
            {
                enemyRiseDamage[i] = riseDamageValue;
            }
            if (float.TryParse(data[i]["EnemyRiseHp"].ToString(), out float riseHealthValue))
            {
                enemyRiseHealth[i] = riseHealthValue;
            }
            if (bool.TryParse(data[i]["DoubleBoss"].ToString(), out bool doubleBossValue))
            {
                doubleBoss[i] = doubleBossValue;
            }
        }
    }


}
