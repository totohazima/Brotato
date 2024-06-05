using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class PlayerStatImporter : CustomExcelDataImportBase
{
    public PlayerStatImporter(string _OutPath) : base(_OutPath) { }

    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        PlayerStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<PlayerStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        InfoTable.table = new PlayerStatInfoTable.Data[sheet.LastRowNum];

        string temp = null;
        string riseStatusKey = null;
        PlayerStatInfoTable.Data data = null;

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            ICell cell = null;
            InfoTable.table[i - 1] = new PlayerStatInfoTable.Data();
            data = InfoTable.table[i - 1];

            cell = row.GetCell(0); data.playerCode = (Player.Character)System.Enum.Parse(typeof(Player.Character),(cell == null ? "" : cell.StringCellValue));
            cell = row.GetCell(1); data.name = (cell == null ? "" : cell.StringCellValue);
            cell = row.GetCell(2); data.health = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(3); data.hpRegen = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(4); data.bloodSucking = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(5); data.persentDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(6); data.meleeDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(7); data.rangeDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(8); data.elementalDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(9); data.attackSpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(10); data.criticalChance = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(11); data.engine = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(12); data.range = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(13); data.armor = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(14); data.evasion = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(15); data.accuracy = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(16); data.lucky = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(17); data.harvest = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(18); data.speed = (float)(cell == null ? 0 : cell.NumericCellValue);

            cell = row.GetCell(19); data.consumableHeal = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(20); data.meterialHeal = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(21); data.expGain = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(22); data.magnetRange = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(23); data.priceSale = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(24); data.explosiveDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(25); data.explosiveSize = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(26); data.chain = (int)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(27); data.penetrate = (int)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(28); data.penetrateDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(29); data.bossDamage = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(30); data.knockBack = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(31); data.doubleMeterial = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(32); data.lootInMeterial = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(33); data.freeReroll = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(34); data.tree = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(35); data.enemyAmount = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(36); data.enemySpeed = (float)(cell == null ? 0 : cell.NumericCellValue);
            cell = row.GetCell(37); data.instantMagnet = (float)(cell == null ? 0 : cell.NumericCellValue);

            int index = 0;
            while(true)
            {
                string text;
                cell = row.GetCell(38 + index); text = (cell == null ? "" : cell.StringCellValue);
                if (text != "")
                {
                    index++;
                }
                else
                {
                    break;
                }
            }
            data.itemTags = new Stat.ItemTag[index];
            for (int z = 0; z < index; z++)
            {
                string text;
                cell = row.GetCell(38 + z); text = (cell == null ? "" : cell.StringCellValue);
                Stat.ItemTag tag = (Stat.ItemTag)System.Enum.Parse(typeof(Stat.ItemTag), text);
                data.itemTags[z] = tag;
            }
        }
        EditorUtility.SetDirty(InfoTable);
    }
    //    public static PlayerStatImporter instance;

    //    [HideInInspector] public int[] characterNum;
    //    [HideInInspector] public string[] characterName;
    //    [HideInInspector] public float[] maxHealth;
    //    [HideInInspector] public float[] regeneration;
    //    [HideInInspector] public float[] bloodSucking;
    //    [HideInInspector] public float[] persentDamage;
    //    [HideInInspector] public float[] meleeDamage;
    //    [HideInInspector] public float[] rangeDamage;
    //    [HideInInspector] public float[] attackSpeed;
    //    [HideInInspector] public float[] criticalChance;
    //    [HideInInspector] public float[] engine;
    //    [HideInInspector] public float[] range;
    //    [HideInInspector] public float[] armor;
    //    [HideInInspector] public float[] evasion;
    //    [HideInInspector] public float[] accuracy;
    //    public float[] speed;

    //    void Awake()
    //    {
    //        instance = this;
    //        string filePath;
    //#if UNITY_ANDROID && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath, "PlayerStat.xlsx");
    //#elif UNITY_IOS && !UNITY_EDITOR
    //            filePath = Path.Combine(Application.persistentDataPath, "PlayerStat.xlsx");
    //#else
    //        filePath = "Assets/Resources/CSV.data/StatInfo/PlayerStat.xlsx";
    //#endif
    //        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("PlayerStat");

    //        characterNum = new int[data.Count];
    //        characterName = new string[data.Count];
    //        maxHealth = new float[data.Count];
    //        regeneration = new float[data.Count];
    //        bloodSucking = new float[data.Count];
    //        persentDamage = new float[data.Count];
    //        meleeDamage = new float[data.Count];
    //        rangeDamage = new float[data.Count];
    //        attackSpeed = new float[data.Count];
    //        criticalChance = new float[data.Count];
    //        engine = new float[data.Count];
    //        range = new float[data.Count];
    //        armor = new float[data.Count];
    //        evasion = new float[data.Count];
    //        accuracy = new float[data.Count];
    //        speed = new float[data.Count];

    //        for (int i = 0; i < data.Count; i++)
    //        {
    //            characterNum[i] = (int)data[i][0];
    //            characterName[i] = (string)data[i][1];

    //            if (float.TryParse(data[i][2].ToString(), out float healthValue))
    //            {
    //                maxHealth[i] = healthValue;
    //            }

    //            if (float.TryParse(data[i][3].ToString(), out float regenerationValue))
    //            {
    //                regeneration[i] = regenerationValue;
    //            }

    //            if (float.TryParse(data[i][4].ToString(), out float bloodSuckingValue))
    //            {
    //                bloodSucking[i] = bloodSuckingValue;
    //            }

    //            if (float.TryParse(data[i][5].ToString(), out float persentDamageValue))
    //            {
    //                persentDamage[i] = persentDamageValue;
    //            }

    //            if (float.TryParse(data[i][6].ToString(), out float meleeDamageValue))
    //            {
    //                meleeDamage[i] = meleeDamageValue;
    //            }

    //            if (float.TryParse(data[i][7].ToString(), out float rangeDamageValue))
    //            {
    //                rangeDamage[i] = rangeDamageValue;
    //            }

    //            if (float.TryParse(data[i][8].ToString(), out float attackSpeedValue))
    //            {
    //                attackSpeed[i] = attackSpeedValue;
    //            }

    //            if (float.TryParse(data[i][9].ToString(), out float criticalChanceValue))
    //            {
    //                criticalChance[i] = criticalChanceValue;
    //            }

    //            if (float.TryParse(data[i][10].ToString(), out float engineValue))
    //            {
    //                engine[i] = engineValue;
    //            }

    //            if (float.TryParse(data[i][11].ToString(), out float rangeValue))
    //            {
    //                range[i] = rangeValue;
    //            }

    //            if (float.TryParse(data[i][12].ToString(), out float armorValue))
    //            {
    //                armor[i] = armorValue;
    //            }

    //            if (float.TryParse(data[i][13].ToString(), out float evasionValue))
    //            {
    //                evasion[i] = evasionValue;
    //            }

    //            if (float.TryParse(data[i][14].ToString(), out float accuracyValue))
    //            {
    //                accuracy[i] = accuracyValue;
    //            }

    //            if (float.TryParse(data[i][15].ToString(), out float speedValue))
    //            {
    //                speed[i] = speedValue;
    //            }
    //        }
    //    }
}
