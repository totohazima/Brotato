using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;

public class WeaponBasePiceImporter : CustomExcelDataImportBase
{
    public WeaponBasePiceImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        WeaponBasePriceInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<WeaponBasePriceInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new WeaponBasePriceInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        WeaponBasePriceInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new WeaponBasePriceInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.weaponCode = (cell == null ? "" : cell.StringCellValue);

            data.weaponBasePrice = new float[4];
            for (int j = 0; j < 4; j++)
            {
                cell = row.GetCell(1 + j); data.weaponBasePrice[j] = (float)(cell == null ? 0 : cell.NumericCellValue);
            }

        }
        EditorUtility.SetDirty(InfoTable);
    }
}
