using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatImporter : MonoBehaviour
{
    public static ItemStatImporter instance;

    [HideInInspector] public string[] itemCode;
    [HideInInspector] public string[] itemName;
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
        itemName = new string[data.Count];
        maxCount = new int[data.Count];

        riseCount = new int[data.Count];
        descendCount = new int[data.Count];
        riseStatType = new string[data.Count];
        descendStatType = new string[data.Count];
        riseStats = new float[data.Count];
        descendStats = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            itemCode[i] = (string)data[i]["ItemCode"];
            itemName[i] = (string)data[i]["ItemName"];
            maxCount[i] = (int)data[i]["MaxCount"];

            riseCount[i] = (int)data[i]["RiseCount"];
            descendCount[i] = (int)data[i]["DescendCount"];
            riseStatType[i] = (string)data[i]["RiseStatType"];
            descendStatType[i] = (string)data[i]["DescendStatType"];

            int j = (int)data[i]["RiseStats"];
            riseStats[i] = j;
            j = (int)data[i]["DescendStats"];
            descendStats[i] = j;
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
