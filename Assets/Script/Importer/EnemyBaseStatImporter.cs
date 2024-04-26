using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class EnemyBaseStatImporter : CustomExcelDataImportBase
{

    public EnemyBaseStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        EnemyBaseStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<EnemyBaseStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new EnemyBaseStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        EnemyBaseStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new EnemyBaseStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.monsterCode = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(1); data.baseHp = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(2); data.baseDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(3); data.baseCoolTime = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(4); data.baseArmor = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(5); data.baseRange = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(6); data.baseEvasion = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(7); data.baseAccuracy = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(8); data.baseMinSpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(9); data.baseMaxSpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(10); data.baseMoneyDropCount = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(11); data.baseMoneyValue = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(12); data.baseExp = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(13); data.baseConsumableDropPersent = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(14); data.baseLootDropPersent = (float)(cell == null ? 0 : cell.NumericCellValue);
        }
        EditorUtility.SetDirty(InfoTable);
    }
    //public static EnemyBaseStatImporter instance;

    //[HideInInspector] public string[] enemyName;
    //[HideInInspector] public float[] health;
    //[HideInInspector] public float[] damage;
    //[HideInInspector] public float[] coolTime;
    //[HideInInspector] public float[] armor;
    //[HideInInspector] public float[] range;
    //[HideInInspector] public float[] evasion;
    //[HideInInspector] public float[] accuracy;
    //[HideInInspector] public float[] minSpeed;
    //[HideInInspector] public float[] maxSpeed;
    //[HideInInspector] public float[] moneyDropNum;
    //[HideInInspector] public int[] moneyValue;
    //[HideInInspector] public int[] expValue;
    //[HideInInspector] public float[] consumDropRate;
    //[HideInInspector] public float[] LootDropRate;

    //void Awake()
    //{
    //    instance = this;

    //    List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("EnemyBaseStat");

    //    enemyName = new string[data.Count];
    //    health = new float[data.Count];
    //    damage = new float[data.Count];
    //    coolTime = new float[data.Count];
    //    armor = new float[data.Count];
    //    range = new float[data.Count];
    //    evasion = new float[data.Count];
    //    accuracy = new float[data.Count];
    //    minSpeed = new float[data.Count];
    //    maxSpeed = new float[data.Count];
    //    moneyDropNum = new float[data.Count];
    //    moneyValue = new int[data.Count];
    //    expValue = new int[data.Count];
    //    consumDropRate = new float[data.Count];
    //    LootDropRate = new float[data.Count];

    //    for (int i = 0; i < data.Count; i++)
    //    { 
    //        enemyName[i] = (string)data[i][0];

    //        if (float.TryParse(data[i][1].ToString(), out float healthValue))
    //        {
    //            health[i] = healthValue;
    //        }

    //        if (float.TryParse(data[i][2].ToString(), out float damageValue))
    //        {
    //            damage[i] = damageValue;
    //        }

    //        if (float.TryParse(data[i][3].ToString(), out float coolTimeValue))
    //        {
    //            coolTime[i] = coolTimeValue;
    //        }

    //        if (float.TryParse(data[i][4].ToString(), out float armorValue))
    //        {
    //            armor[i] = armorValue;
    //        }

    //        if (float.TryParse(data[i][5].ToString(), out float rangeValue))
    //        {
    //            range[i] = rangeValue;
    //        }

    //        if (float.TryParse(data[i][6].ToString(), out float evasionValue))
    //        {
    //            evasion[i] = evasionValue;
    //        }

    //        if (float.TryParse(data[i][7].ToString(), out float accuracyValue))
    //        {
    //            accuracy[i] = accuracyValue;
    //        }

    //        if (float.TryParse(data[i][8].ToString(), out float minSpeedValue))
    //        {
    //            minSpeed[i] = minSpeedValue;
    //        }

    //        if (float.TryParse(data[i][9].ToString(), out float maxSpeedValue))
    //        {
    //            maxSpeed[i] = maxSpeedValue;
    //        }

    //        if (float.TryParse(data[i][10].ToString(), out float moneyDropNumValue))
    //        {
    //            moneyDropNum[i] = moneyDropNumValue;
    //        }

    //        if (int.TryParse(data[i][11].ToString(), out int moneyValueValue))
    //        {
    //            moneyValue[i] = moneyValueValue;
    //        }

    //        if (int.TryParse(data[i][12].ToString(), out int expValueValue))
    //        {
    //            expValue[i] = expValueValue;
    //        }

    //        if (float.TryParse(data[i][13].ToString(), out float consumDropRateValue))
    //        {
    //            consumDropRate[i] = consumDropRateValue;
    //        }

    //        if (float.TryParse(data[i][14].ToString(), out float LootDropRateValue))
    //        {
    //            LootDropRate[i] = LootDropRateValue;
    //        }
    //    }
    //}
}
