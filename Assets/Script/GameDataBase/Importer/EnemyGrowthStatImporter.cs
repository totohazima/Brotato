using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;

public class EnemyGrowthStatImporter : CustomExcelDataImportBase
{
    
    public EnemyGrowthStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        EnemyGrowthStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<EnemyGrowthStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new EnemyGrowthStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        EnemyGrowthStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new EnemyGrowthStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.monsterCode = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.hpRisePer = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(2); data.attackRisePer = (float)(cell == null ? 0 : cell.NumericCellValue);
        }
        EditorUtility.SetDirty(InfoTable);
    }

    //    public static EnemyGrowthStatImporter instance;

    //    [HideInInspector] public float[] grow_Health;
    //    [HideInInspector] public float[] grow_Damage;
    //    void Awake()
    //    {
    //        instance = this;
    //        string filePath;
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath, "EnemyGrowthStat.xlsx");
    //#elif UNITY_IOS && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath, "EnemyGrowthStat.xlsx");
    //#else
    //        filePath = "Assets/Resources/CSV.data/StatInfo/EnemyGrowthStat.xlsx";
    //#endif
    //        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("EnemyGrowthStat");
    //        grow_Health = new float[data.Count];
    //        grow_Damage = new float[data.Count];
    //        for (int i = 0; i < data.Count; i++)
    //        {
    //            // 각 열의 데이터를 적절한 형식으로 변환하여 저장
    //            if (float.TryParse(data[i][1].ToString(), out float healthValue))
    //            {
    //                grow_Health[i] = healthValue;
    //            }
    //            if (float.TryParse(data[i][2].ToString(), out float damageValue))
    //            {
    //                grow_Damage[i] = damageValue;
    //            }
    //        }
    //    }
}
