using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;


public class WeaponPercentageImporter : CustomExcelDataImportBase
{
    public WeaponPercentageImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        WeaponPercentageInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<WeaponPercentageInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new WeaponPercentageInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        WeaponPercentageInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new WeaponPercentageInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.tier = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.available_Wave = (int)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(2); data.minWave = (int)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(3); data.baseChance = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(4); data.chancePerWave = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(5); data.maxChance = (float)(cell == null ? 0 : cell.NumericCellValue);

            EditorUtility.SetDirty(InfoTable);
        }
    }
}
