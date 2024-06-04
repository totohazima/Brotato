using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;

public class BossGrowthStatImporter : CustomExcelDataImportBase
{
    
    public BossGrowthStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        BossGrowthStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<BossGrowthStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new BossGrowthStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        BossGrowthStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new BossGrowthStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.monsterCode = (Enemy.EnemyName)System.Enum.Parse(typeof(Enemy.EnemyName), (cell == null ? "" : cell.StringCellValue));
            cell = row.GetCell(1); data.hpRisePer = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(2); data.attackRisePer = (float)(cell == null ? 0 : cell.NumericCellValue);
        }
        EditorUtility.SetDirty(InfoTable);
    }
}
