using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class WaveStatImporter : CustomExcelDataImportBase
{
    public WaveStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        WaveStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<WaveStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new WaveStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        WaveStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new WaveStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.waveName = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.maxEnemySpawn = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(2); data.enemySpawnCount = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(3); data.enemySpawnTime = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(4); data.waveTime = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(5); data.waveTreeStat = (float)(cell == null ? 0 : cell.NumericCellValue);
        }
        EditorUtility.SetDirty(InfoTable);
    }

    //    public static WaveStatImporter instance;

    //    [HideInInspector] public int[] enemyCount;
    //    [HideInInspector] public int[] enemySpawnCount;
    //    [HideInInspector] public float[] waveTime;
    //    [HideInInspector] public float[] treeCount;
    //    public Wave_Scriptable[] wave_Scriptables;
    //    void Awake()
    //    {

    //        instance = this;
    //        string filePath;
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath + "WaveStat.xlsx");
    //#elif UNITY_IOS && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath+ "WaveStat.xlsx");
    //#else
    //        filePath = "Assets/Resources/CSV.data/StatInfo/WaveStat.xlsx";
    //#endif

    //        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("WaveStat");
    //        enemyCount = new int[data.Count];
    //        enemySpawnCount = new int[data.Count];
    //        waveTime = new float[data.Count];
    //        treeCount = new float[data.Count];
    //        for (int i = 0; i < data.Count; i++)
    //        {
    //            if (int.TryParse(data[i][0].ToString(), out int enemyCountValue))
    //            {
    //                enemyCount[i] = enemyCountValue;
    //            }
    //            if (int.TryParse(data[i][1].ToString(), out int enemySpawnCountValue))
    //            {
    //                enemySpawnCount[i] = enemySpawnCountValue;
    //            }
    //            if (float.TryParse(data[i][2].ToString(), out float waveTimeValue))
    //            {
    //                waveTime[i] = waveTimeValue;
    //            }
    //            if (float.TryParse(data[i][3].ToString(), out float treeCountValue))
    //            {
    //                treeCount[i] = treeCountValue;
    //            }
    //        }
    //    }
}
