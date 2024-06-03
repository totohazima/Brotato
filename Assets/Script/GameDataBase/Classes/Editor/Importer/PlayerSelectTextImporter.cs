using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;

public class PlayerSelectTextImporter : CustomExcelDataImportBase
{
    public PlayerSelectTextImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        PlayerSelectTextInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<PlayerSelectTextInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        List<PlayerSelectTextInfoTable.Data> dataList = new List<PlayerSelectTextInfoTable.Data>();

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            string itemName = row.GetCell(0).StringCellValue;

            if (!string.IsNullOrEmpty(itemName) && itemName != "")
            {
                PlayerSelectTextInfoTable.Data data = new PlayerSelectTextInfoTable.Data();
                data.characterCode = (Player.Character)System.Enum.Parse(typeof(Player.Character), itemName);
                data.characterName = (row.GetCell(1)?.StringCellValue ?? "");

                int textCount = (int)(row.GetCell(2)?.NumericCellValue ?? 0);
                data.characterInfo = new string[textCount];
                for (int j = 0; j < textCount; j++)
                {
                    IRow textRow = sheet.GetRow(i + j);
                    data.characterInfo[j] = (textRow.GetCell(3)?.StringCellValue ?? "");
                }

                dataList.Add(data);
            }
        }

        InfoTable.table = dataList.ToArray();
        EditorUtility.SetDirty(InfoTable);
    }
}
