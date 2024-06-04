using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class BossBaseStatImporter : CustomExcelDataImportBase
{

    public BossBaseStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        BossBaseStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<BossBaseStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new BossBaseStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        BossBaseStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new BossBaseStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.monsterCode = (Enemy.EnemyName)System.Enum.Parse(typeof(Enemy.EnemyName), (cell == null ? "" : cell.StringCellValue));
            cell = row.GetCell(1); data.enemyType = (Stat.enemyType)System.Enum.Parse(typeof(Stat.enemyType), (cell == null ? "" : cell.StringCellValue));
            cell = row.GetCell(2); data.baseHp = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(3); data.baseDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(4); data.baseCoolTime = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(5); data.baseArmor = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(6); data.baseRange = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(7); data.baseEvasion = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(8); data.baseAccuracy = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(9); data.baseMinSpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(10); data.baseMaxSpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(11); data.baseMoneyDropCount = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(12); data.baseMoneyValue = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(13); data.baseExp = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(14); data.baseConsumableDropPersent = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(15); data.baseLootDropPersent = (float)(cell == null ? 0 : cell.NumericCellValue);
        }
        EditorUtility.SetDirty(InfoTable);
    }
    
}
