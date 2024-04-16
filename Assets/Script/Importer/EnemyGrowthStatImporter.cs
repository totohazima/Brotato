using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGrowthStatImporter : MonoBehaviour
{
    public static EnemyGrowthStatImporter instance;

    [HideInInspector] public float[] grow_Health;
    [HideInInspector] public float[] grow_Damage;
    void Awake()
    {
        instance = this;
        string filePath = "Assets/Resources/CSV.data/StatInfo/EnemyGrowthStat.xlsx";
        List<Dictionary<int, object>> data = ExcelReader.ReadNumericColumns(filePath);
        grow_Health = new float[data.Count];
        grow_Damage = new float[data.Count];
        for (int i = 0; i < data.Count; i++)
        {
            // 각 열의 데이터를 적절한 형식으로 변환하여 저장
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
