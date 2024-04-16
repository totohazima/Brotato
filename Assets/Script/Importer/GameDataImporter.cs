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
            // �� ���� �����͸� ������ �������� ��ȯ�Ͽ� ����
            if (bool.TryParse(data[i]["�ѱ�"].ToString(), out bool value1))
            {
                korean1[i] = value1;
            }
            if (int.TryParse(data[i]["�ѱ�3"].ToString(), out int value2))
            {
                korean3[i] = value2;
            }
            if (float.TryParse(data[i]["�ѱ�4"].ToString(), out float value3))
            {
                korean4[i] = value3;
            }
        }
    }
}
