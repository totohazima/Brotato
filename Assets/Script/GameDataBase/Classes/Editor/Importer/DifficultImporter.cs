using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class DifficultImporter : CustomExcelDataImportBase
{
    public DifficultImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        DifficultInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<DifficultInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new DifficultInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        DifficultInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new DifficultInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.difficultName = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.specialEnemySpawn = (cell == null ? false : cell.BooleanCellValue);
            cell = row.GetCell(2); data.eliteEnemySpawn = (cell == null ? false : cell.BooleanCellValue);
            cell = row.GetCell(3); data.eliteEnemySpawnCount = (int)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(4); data.enemyDamageRise = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(5); data.enemyHpRise = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(6); data.twinBoss = (cell == null ? false : cell.BooleanCellValue);

        }
        EditorUtility.SetDirty(InfoTable);
    }
}
