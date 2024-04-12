using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultImporter : MonoBehaviour
{
    public static DifficultImporter instance;

    public bool[] isSpecialEnemy;//���ο� ���� ����
    public bool[] isEliteSpawn; //����Ʈ�� ������ ����
    public int[] isEliteWaveCount; //����Ʈ�� ������ �����ϴ� ���̺� ��(6���� ���� ����)
    public float[] enemyRiseDamage; //�� ������ ����ġ %
    public float[] enemyRiseHealth; //�� ü�� ����ġ %
    public bool[] doubleBoss; //������ 2���� (ü���� 25% ����)
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
