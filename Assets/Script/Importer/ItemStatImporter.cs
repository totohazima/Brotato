using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatImporter : MonoBehaviour
{
    public static ItemStatImporter instance;

    [HideInInspector] public string[] itemCode;
    [HideInInspector] public int[] maxCount;

    [HideInInspector] public int[] riseCount;
    [HideInInspector] public string[] riseStatType;
    [HideInInspector] public float[] riseStats;

    [HideInInspector] public int[] descendCount;
    [HideInInspector] public string[] descendStatType;
    [HideInInspector] public float[] descendStats;

    public string[] itemCode2;
    public int[] infoCount;
    public string[] infoText;

    void Awake()
    {
        instance = this;

        List<Dictionary<string, object>> data = CSVReaderStat.Read("ItemStatInfo");
        itemCode = new string[data.Count];
        maxCount = new int[data.Count];

        riseCount = new int[data.Count];
        descendCount = new int[data.Count];
        riseStatType = new string[data.Count];
        descendStatType = new string[data.Count];
        riseStats = new float[data.Count];
        descendStats = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {

            // 각 열의 데이터를 적절한 형식으로 변환하여 저장
            itemCode[i] = (string)data[i]["ItemCode"];
            if (int.TryParse(data[i]["MaxCount"].ToString(), out int maxCountValue))
            {
                maxCount[i] = maxCountValue;
            }

            if (int.TryParse(data[i]["RiseCount"].ToString(), out int riseCountValue))
            {
                riseCount[i] = riseCountValue;
            }
            riseStatType[i] = (string)data[i]["RiseStatType"];
            if (int.TryParse(data[i]["DescendCount"].ToString(), out int descendCountValue))
            {
                descendCount[i] = descendCountValue;
            }
            descendStatType[i] = (string)data[i]["DescendStatType"];

            if (float.TryParse(data[i]["RiseStats"].ToString(), out float riseStatsValue))
            {
                riseStats[i] = riseStatsValue;
            }

            if (float.TryParse(data[i]["DescendStats"].ToString(), out float descendStatsValue))
            {
                descendStats[i] = descendStatsValue;
            }
        }

        List<Dictionary<string, object>> data1 = CSVReaderText.Read("ItemText");
        itemCode2 = new string[data1.Count];
        infoCount = new int[data1.Count];
        infoText = new string[data1.Count];

        for (int i = 0; i < data1.Count; i++)
        {
            itemCode2[i] = (string)data1[i]["ItemCode"];
            infoCount[i] = (int)data1[i]["TextCount"];
            infoText[i] = (string)data1[i]["Text"];
        }
    }
}