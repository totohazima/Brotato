using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class ItemBasePriceImporter : CustomExcelDataImportBase
{
    public ItemBasePriceImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        ItemBasePriceInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<ItemBasePriceInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new ItemBasePriceInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        ItemBasePriceInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new ItemBasePriceInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.itemCode = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.itemBasePrice = (float)(cell == null ? 0 : cell.NumericCellValue);

        }
        EditorUtility.SetDirty(InfoTable);
    }
}
