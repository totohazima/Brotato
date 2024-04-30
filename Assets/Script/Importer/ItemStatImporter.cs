using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class ItemStatImporter : CustomExcelDataImportBase
{
    public ItemStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        ItemStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<ItemStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        List<ItemStatInfoTable.Data> dataList = new List<ItemStatInfoTable.Data>();

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            string itemName = row.GetCell(0).StringCellValue;

            if (!string.IsNullOrEmpty(itemName) && itemName != ".")
            {
                ItemStatInfoTable.Data data = new ItemStatInfoTable.Data();
                data.itemCode = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), itemName);
                data.itemMaxCount = (int)(row.GetCell(1)?.NumericCellValue ?? 0);
                data.statRiseCount = (int)(row.GetCell(2)?.NumericCellValue ?? 0);
                data.statDecreaseCount = (int)(row.GetCell(3)?.NumericCellValue ?? 0);

                data.riseStatCode = new string[data.statRiseCount];
                data.riseNum = new float[data.statRiseCount];
                data.decreaseStatCode = new string[data.statDecreaseCount];
                data.decreaseNum = new float[data.statDecreaseCount];

                for (int j = 0; j < data.statRiseCount; j++)
                {
                    IRow riseRow = sheet.GetRow(i + j);
                    data.riseStatCode[j] = (riseRow.GetCell(4)?.StringCellValue ?? "");
                    data.riseNum[j] = (float)(riseRow.GetCell(5)?.NumericCellValue ?? 0);
                }

                for (int j = 0; j < data.statDecreaseCount; j++)
                {
                    IRow decreaseRow = sheet.GetRow(i + j);
                    data.decreaseStatCode[j] = (decreaseRow.GetCell(6)?.StringCellValue ?? "");
                    data.decreaseNum[j] = (float)(decreaseRow.GetCell(7)?.NumericCellValue ?? 0);
                }

                dataList.Add(data);
            }
        }

        InfoTable.table = dataList.ToArray();
        EditorUtility.SetDirty(InfoTable);
    }
    
    //    public static ItemStatImporter instance;

    //    [HideInInspector] public string[] itemCode;
    //    [HideInInspector] public int[] maxCount;

    //    [HideInInspector] public int[] riseCount;
    //    [HideInInspector] public string[] riseStatType;
    //    [HideInInspector] public float[] riseStats;

    //    [HideInInspector] public int[] descendCount;
    //    [HideInInspector] public string[] descendStatType;
    //    [HideInInspector] public float[] descendStats;

    //    [HideInInspector] public string[] itemCode2;
    //    [HideInInspector] public int[] infoCount;
    //    [HideInInspector] public string[] infoText;

    //    void Awake()
    //    {
    //        instance = this;
    //        string filePath;
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath, "ItemStat.xlsx");
    //#elif UNITY_IOS && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath, "ItemStat.xlsx");
    //#else
    //        filePath = "Assets/Resources/CSV.data/StatInfo/ItemStat.xlsx";
    //#endif
    //        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("ItemStat");
    //        itemCode = new string[data.Count];
    //        maxCount = new int[data.Count];

    //        riseCount = new int[data.Count];
    //        descendCount = new int[data.Count];
    //        riseStatType = new string[data.Count];
    //        descendStatType = new string[data.Count];
    //        riseStats = new float[data.Count];
    //        descendStats = new float[data.Count];

    //        for (int i = 0; i < data.Count; i++)
    //        {

    //            // 각 열의 데이터를 적절한 형식으로 변환하여 저장
    //            itemCode[i] = (string)data[i][0];
    //            if (int.TryParse(data[i][1].ToString(), out int maxCountValue))
    //            {
    //                maxCount[i] = maxCountValue;
    //            }

    //            if (int.TryParse(data[i][2].ToString(), out int riseCountValue))
    //            {
    //                riseCount[i] = riseCountValue;
    //            }
    //            riseStatType[i] = (string)data[i][4];

    //            if (int.TryParse(data[i][3].ToString(), out int descendCountValue))
    //            {
    //                descendCount[i] = descendCountValue;
    //            }
    //            descendStatType[i] = (string)data[i][6];

    //            if (float.TryParse(data[i][5].ToString(), out float riseStatsValue))
    //            {
    //                riseStats[i] = riseStatsValue;
    //            }

    //            if (float.TryParse(data[i][7].ToString(), out float descendStatsValue))
    //            {
    //                descendStats[i] = descendStatsValue;
    //            }
    //        }

    //        List<Dictionary<string, object>> data1 = CSVReaderText.Read("ItemText");
    //        itemCode2 = new string[data1.Count];
    //        infoCount = new int[data1.Count];
    //        infoText = new string[data1.Count];

    //        for (int i = 0; i < data1.Count; i++)
    //        {
    //            itemCode2[i] = (string)data1[i]["ItemCode"];
    //            infoCount[i] = (int)data1[i]["TextCount"];
    //            infoText[i] = (string)data1[i]["Text"];
    //        }
    //    }
}