using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class UpgradeStatImporter : CustomExcelDataImportBase
{
    public UpgradeStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        UpgradeStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<UpgradeStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new UpgradeStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        UpgradeStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new UpgradeStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.upgradeCode = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.upgradeName = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(2); data.upgradeEffect = (cell == null ? "" : cell.StringCellValue);

            data.tierEffect = new float[4];
            for (int j = 0; j < data.tierEffect.Length; j++)
            {
                cell = row.GetCell(j + 3); data.tierEffect[j] = (float)(cell == null ? 0 : cell.NumericCellValue);

            }
            EditorUtility.SetDirty(InfoTable);
        }
        //    public static UpgradeStatImporter instance;

        //    [HideInInspector] public int[] upgradeNum;
        //    [HideInInspector] public string[] upgradeName;
        //    [HideInInspector] public string[] upgradeEffect;
        //    [HideInInspector] public float[] heart;
        //    [HideInInspector] public float[] lungs;
        //    [HideInInspector] public float[] teeth;
        //    [HideInInspector] public float[] triceps;
        //    [HideInInspector] public float[] forearms;
        //    [HideInInspector] public float[] shoulders;
        //    [HideInInspector] public float[] reflexes;
        //    [HideInInspector] public float[] fingers;
        //    [HideInInspector] public float[] skull;
        //    [HideInInspector] public float[] eyes;
        //    [HideInInspector] public float[] chest;
        //    [HideInInspector] public float[] back;
        //    [HideInInspector] public float[] legs;

        //    void Awake()
        //    {
        //        instance = this;
        //        string filePath;
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //            filePath = Path.Combine(Application.persistentDataPath, "UpgradeStat.xlsx");
        //#elif UNITY_IOS && !UNITY_EDITOR
        //            filePath = Path.Combine(Application.persistentDataPath, "UpgradeStat.xlsx");
        //#else
        //        filePath = "Assets/Resources/CSV.data/StatInfo/UpgradeStat.xlsx";
        //#endif
        //        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("UpgradeStat");
        //        upgradeNum = new int[data.Count];
        //        upgradeName = new string[data.Count];
        //        upgradeEffect = new string[data.Count];
        //        heart = new float[4];
        //        lungs = new float[4];
        //        teeth = new float[4];
        //        triceps = new float[4];
        //        forearms = new float[4];
        //        shoulders = new float[4];
        //        reflexes = new float[4];
        //        fingers = new float[4];
        //        skull = new float[4];
        //        eyes = new float[4];
        //        chest = new float[4];
        //        back = new float[4];
        //        legs = new float[4];

        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            upgradeNum[i] = (int)data[i][0];
        //            upgradeName[i] = (string)data[i][1];
        //            upgradeEffect[i] = (string)data[i][2];
        //        }
        //        for (int i = 0; i < 4; i++)
        //        {
        //            if (float.TryParse(data[0][i + 3].ToString(), out float heartValue))
        //            {
        //                heart[i] = heartValue;
        //            }
        //            if (float.TryParse(data[1][i + 3].ToString(), out float lungsValue))
        //            {
        //                lungs[i] = lungsValue;
        //            }
        //            if (float.TryParse(data[2][i + 3].ToString(), out float teethtValue))
        //            {
        //                teeth[i] = teethtValue;
        //            }
        //            if (float.TryParse(data[3][i + 3].ToString(), out float tricepsValue))
        //            {
        //                triceps[i] = tricepsValue;
        //            }
        //            if (float.TryParse(data[4][i + 3].ToString(), out float forearmsValue))
        //            {
        //                forearms[i] = forearmsValue;
        //            }
        //            if (float.TryParse(data[5][i + 3].ToString(), out float shouldersValue))
        //            {
        //                shoulders[i] = shouldersValue;
        //            }
        //            if (float.TryParse(data[6][i + 3].ToString(), out float reflexesValue))
        //            {
        //                reflexes[i] = reflexesValue;
        //            }
        //            if (float.TryParse(data[7][i + 3].ToString(), out float fingersValue))
        //            {
        //                fingers[i] = fingersValue;
        //            }
        //            if (float.TryParse(data[8][i + 3].ToString(), out float skullValue))
        //            {
        //                skull[i] = skullValue;
        //            }
        //            if (float.TryParse(data[9][i + 3].ToString(), out float eyesValue))
        //            {
        //                eyes[i] = eyesValue;
        //            }
        //            if (float.TryParse(data[10][i + 3].ToString(), out float chestValue))
        //            {
        //                chest[i] = chestValue;
        //            }
        //            if (float.TryParse(data[11][i + 3].ToString(), out float backValue))
        //            {
        //                back[i] = backValue;
        //            }
        //            if (float.TryParse(data[12][i + 3].ToString(), out float legsValue))
        //            {
        //                legs[i] = legsValue;
        //            }
        //        }
        //    }

    }
}
