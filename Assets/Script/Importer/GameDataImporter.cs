using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataImporter : MonoBehaviour
{
    public bool[] korean1;
    public int[] korean3;
    public float[] korean4;

    void Awake()
    {
        string filePath = "Assets/Resources/CSV.data/StatInfo/testXLSX.xlsx";
        List<Dictionary<string, object>> data = ExcelReader.Read(filePath);
        korean1 = new bool[data.Count];
        korean3 = new int[data.Count];
        korean4 = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            // 각 열의 데이터를 적절한 형식으로 변환하여 저장
            if (bool.TryParse(data[i]["한글"].ToString(), out bool value1))
            {
                korean1[i] = value1;
            }
            if (int.TryParse(data[i]["한글3"].ToString(), out int value2))
            {
                korean3[i] = value2;
            }
            if (float.TryParse(data[i]["한글4"].ToString(), out float value3))
            {
                korean4[i] = value3;
            }
        }
    }
}
