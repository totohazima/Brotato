using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;

public class PlayerAchivementImporter : CustomExcelDataImportBase
{
    public PlayerAchivementImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        PlayerAchivementInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<PlayerAchivementInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new PlayerAchivementInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        PlayerAchivementInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new PlayerAchivementInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.characterCode = (Player.Character)System.Enum.Parse(typeof(Player.Character),(cell == null ? "" : cell.StringCellValue));
            cell = row.GetCell(1); data.characterAchivement_Text = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(2); data.isEarlyUnlock = (cell == null ? false : cell.BooleanCellValue);


            EditorUtility.SetDirty(InfoTable);
        }
    }
}
