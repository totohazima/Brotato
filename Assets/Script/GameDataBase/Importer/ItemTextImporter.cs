using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class ItemTextImporter : CustomExcelDataImportBase
{
    public ItemTextImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        ItemTextInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<ItemTextInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        List<ItemTextInfoTable.Data> dataList = new List<ItemTextInfoTable.Data>();

        int sheetLastRow = 106;
        for (int i = 1; i <= sheetLastRow; i++)
        {
            IRow row = sheet.GetRow(i);
            string itemName = row.GetCell(0).StringCellValue;

            if (!string.IsNullOrEmpty(itemName) && itemName != ".")
            {
                ItemTextInfoTable.Data data = new ItemTextInfoTable.Data();
                data.itemCode = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), itemName);
                data.textCount = (int)(row.GetCell(1)?.NumericCellValue ?? 0);

                data.text = new string[data.textCount];
                for (int j = 0; j < data.text.Length; j++)
                {
                    IRow textRow = sheet.GetRow(i + j);
                    data.text[j] = (textRow.GetCell(2)?.StringCellValue ?? "");
                }

                dataList.Add(data);
            }
        }

        InfoTable.table = dataList.ToArray();
        EditorUtility.SetDirty(InfoTable);
    }

}
