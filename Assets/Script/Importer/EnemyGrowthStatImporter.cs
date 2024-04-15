using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrowthStatImporter : MonoBehaviour
{
    public static EnemyGrowthStatImporter instance;

    public float[] grow_Health;
    public float[] grow_Damage;
    void Awake()
    {
        instance = this;
        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("EnemyGrowthStat");
        grow_Health = new float[data.Count];
        grow_Damage = new float[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            // �� ���� �����͸� ������ �������� ��ȯ�Ͽ� ����
            if (float.TryParse(data[i][1].ToString(), out float healthValue))
            {
                grow_Health[i] = healthValue;
            }
            if (float.TryParse(data[i][2].ToString(), out float damageValue))
            {
                grow_Damage[i] = damageValue;
            }
        }
    }
}
