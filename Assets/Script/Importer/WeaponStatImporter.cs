using System.Collections;
using System.Collections.Generic;
using Only1Games.GDBA;
using NPOI.SS.UserModel;
using UnityEditor;
using UnityEngine;
public class WeaponStatImporter : CustomExcelDataImportBase
{
    public WeaponStatImporter(string _OutPath) : base(_OutPath) { }


    public override void ImporteExcel(string _excelName, ISheet sheet)
    {
        WeaponStatInfoTable InfoTable = ExcelDataImporter.LoadOrCreateAsset<WeaponStatInfoTable>("Assets/Resources/CSV.data/GameInfoData/", _excelName, HideFlags.None);
        List<WeaponStatInfoTable.Data> dataList = new List<WeaponStatInfoTable.Data>();

        for (int i = 1; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            string weaponCode = row.GetCell(1).StringCellValue;

            if (!string.IsNullOrEmpty(weaponCode) && weaponCode != "")
            {
                WeaponStatInfoTable.Data data = new WeaponStatInfoTable.Data();
                data.weaponNum = (int)(row.GetCell(0)?.NumericCellValue ?? 0);
                data.weaponCode = weaponCode;

                data.weaponBaseDamage = new float[4];
                for (int j = 0; j < data.weaponBaseDamage.Length; j++)
                {
                    data.weaponBaseDamage[j] = (float)(row.GetCell(2 + j)?.NumericCellValue ?? 0);
                }

                data.weaponBaseBulletCount = new int[4];
                for (int j = 0; j < data.weaponBaseBulletCount.Length; j++)
                {
                    data.weaponBaseBulletCount[j] = (int)(row.GetCell(6 + j)?.NumericCellValue ?? 0);
                }

                data.riseDamageCount = (int)(row.GetCell(10)?.NumericCellValue ?? 0);
                data.riseDamageType = new string[data.riseDamageCount];
                for (int j = 0; j < data.riseDamageCount; j++)
                {
                    IRow riseRow = sheet.GetRow(i + j);
                    data.riseDamageType[j] = (riseRow.GetCell(11)?.StringCellValue ?? "");
                }

                data.riseDamage1 = new float[data.riseDamageCount];
                data.riseDamage2 = new float[data.riseDamageCount];
                data.riseDamage3 = new float[data.riseDamageCount];
                data.riseDamage4 = new float[data.riseDamageCount];
                for (int j = 0; j < data.riseDamageCount; j++)
                {
                    IRow riseRow = sheet.GetRow(i + j);
                    data.riseDamage1[j] = (float)(riseRow.GetCell(12)?.NumericCellValue ?? 0);
                    data.riseDamage2[j] = (float)(riseRow.GetCell(13)?.NumericCellValue ?? 0);
                    data.riseDamage3[j] = (float)(riseRow.GetCell(14)?.NumericCellValue ?? 0);
                    data.riseDamage4[j] = (float)(riseRow.GetCell(15)?.NumericCellValue ?? 0);
                }

                data.baseCriticalChance = new float[4];
                data.baseCriticalDamage = new float[4];
                data.baseCoolTime = new float[4];
                data.baseKnockback = new float[4];
                data.baseRange = new float[4];
                for (int j = 0; j < 4; j++)
                {
                    data.baseCriticalChance[j] = (float)(row.GetCell(16 + j)?.NumericCellValue ?? 0);
                    data.baseCriticalDamage[j] = (float)(row.GetCell(20 + j)?.NumericCellValue ?? 0);
                    data.baseCoolTime[j] = (float)(row.GetCell(24 + j)?.NumericCellValue ?? 0);
                    data.baseKnockback[j] = (float)(row.GetCell(28 + j)?.NumericCellValue ?? 0);
                    data.baseRange[j] = (float)(row.GetCell(32 + j)?.NumericCellValue ?? 0);
                }
                data.penetration = (int)(row.GetCell(36)?.NumericCellValue ?? 0);
                data.penetrationDamage = (float)(row.GetCell(37)?.NumericCellValue ?? 0);
                data.attackType = (row.GetCell(38)?.StringCellValue ?? "");
                data.weaponType = (row.GetCell(39)?.StringCellValue ?? "");

                dataList.Add(data);
            }
        }

        InfoTable.table = dataList.ToArray();
        EditorUtility.SetDirty(InfoTable);
    }
    //public static WeaponStatImporter instance;

    //[HideInInspector] public int[] weaponNum;
    //[HideInInspector] public string[] name;
    //[HideInInspector] public float[] damage1, damage2, damage3, damage4;
    //[HideInInspector] public int[] bulletCount1, bulletCount2, bulletCount3, bulletCount4;
    //[HideInInspector] public int[] multipleDamageCount;
    //[HideInInspector] public string[] multipleDamageType;
    //[HideInInspector] public float[] multipleDamage1, multipleDamage2, multipleDamage3, multipleDamage4;
    //[HideInInspector] public float[] criticalChance1, criticalChance2, criticalChance3, criticalChance4;
    //[HideInInspector] public float[] criticalDamage1, criticalDamage2, criticalDamage3, criticalDamage4;
    //[HideInInspector] public float[] coolTIme1, coolTIme2, coolTIme3, coolTIme4;
    //[HideInInspector] public float[] knockBack1, knockBack2, knockBack3, knockBack4;
    //[HideInInspector] public float[] range1, range2, range3, range4;
    //[HideInInspector] public int[] penetrate;
    //[HideInInspector] public float[] penetrateDamage;
    //[HideInInspector] public int[] type;
    //[HideInInspector] public string[] weaponType;

    //void Awake()
    //{
    //    instance = this;
    //    string filePath = "Assets/Resources/CSV.data/StatInfo/WeaponStat.xlsx";
    //    List<Dictionary<string, object>> data = CSVReaderStat.Read("WeaponStat");
    //    weaponNum = new int[data.Count];
    //    name = new string[data.Count];

    //    damage1 = new float[data.Count];
    //    damage2 = new float[data.Count];
    //    damage3 = new float[data.Count];
    //    damage4 = new float[data.Count];

    //    bulletCount1 = new int[data.Count];
    //    bulletCount2 = new int[data.Count];
    //    bulletCount3 = new int[data.Count];
    //    bulletCount4 = new int[data.Count];

    //    multipleDamageCount = new int[data.Count];
    //    multipleDamageType = new string[data.Count];

    //    multipleDamage1 = new float[data.Count];
    //    multipleDamage2 = new float[data.Count];
    //    multipleDamage3 = new float[data.Count];
    //    multipleDamage4 = new float[data.Count];

    //    criticalChance1 = new float[data.Count];
    //    criticalChance2 = new float[data.Count];
    //    criticalChance3 = new float[data.Count];
    //    criticalChance4 = new float[data.Count];

    //    criticalDamage1 = new float[data.Count];
    //    criticalDamage2 = new float[data.Count];
    //    criticalDamage3 = new float[data.Count];
    //    criticalDamage4 = new float[data.Count];

    //    coolTIme1 = new float[data.Count];
    //    coolTIme2 = new float[data.Count];
    //    coolTIme3 = new float[data.Count];
    //    coolTIme4 = new float[data.Count];

    //    knockBack1 = new float[data.Count];
    //    knockBack2 = new float[data.Count];
    //    knockBack3 = new float[data.Count];
    //    knockBack4 = new float[data.Count];

    //    range1 = new float[data.Count];
    //    range2 = new float[data.Count];
    //    range3 = new float[data.Count];
    //    range4 = new float[data.Count];

    //    penetrate = new int[data.Count];
    //    penetrateDamage = new float[data.Count];
    //    type = new int[data.Count];
    //    weaponType = new string[data.Count];

    //    for(int i = 0; i < data.Count; i++)
    //    {
    //        weaponNum[i] = (int)data[i]["WeaponNum"];
    //        name[i] = (string)data[i]["WeaponCode"];

    //        // TryParse를 사용하여 각 열의 데이터를 적절한 형식으로 변환하여 저장
    //        if (float.TryParse(data[i]["Damage1"].ToString(), out float damage1Value))
    //        {
    //            damage1[i] = damage1Value;
    //        }

    //        if (float.TryParse(data[i]["Damage2"].ToString(), out float damage2Value))
    //        {
    //            damage2[i] = damage2Value;
    //        }

    //        if (float.TryParse(data[i]["Damage3"].ToString(), out float damage3Value))
    //        {
    //            damage3[i] = damage3Value;
    //        }

    //        if (float.TryParse(data[i]["Damage4"].ToString(), out float damage4Value))
    //        {
    //            damage4[i] = damage4Value;
    //        }

    //        bulletCount1[i] = (int)data[i]["BulletCount1"];
    //        bulletCount2[i] = (int)data[i]["BulletCount2"];
    //        bulletCount3[i] = (int)data[i]["BulletCount3"];
    //        bulletCount4[i] = (int)data[i]["BulletCount4"];

    //        multipleDamageCount[i] = (int)data[i]["MultipleDamageCount"];
    //        multipleDamageType[i] = (string)data[i]["MultipleDamageType"];

    //        if (float.TryParse(data[i]["MultipleDamage1"].ToString(), out float multipleDamage1Value))
    //        {
    //            multipleDamage1[i] = multipleDamage1Value;
    //        }

    //        if (float.TryParse(data[i]["MultipleDamage2"].ToString(), out float multipleDamage2Value))
    //        {
    //            multipleDamage2[i] = multipleDamage2Value;
    //        }

    //        if (float.TryParse(data[i]["MultipleDamage3"].ToString(), out float multipleDamage3Value))
    //        {
    //            multipleDamage3[i] = multipleDamage3Value;
    //        }

    //        if (float.TryParse(data[i]["MultipleDamage4"].ToString(), out float multipleDamage4Value))
    //        {
    //            multipleDamage4[i] = multipleDamage4Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalChance1"].ToString(), out float criticalChance1Value))
    //        {
    //            criticalChance1[i] = criticalChance1Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalChance2"].ToString(), out float criticalChance2Value))
    //        {
    //            criticalChance2[i] = criticalChance2Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalChance3"].ToString(), out float criticalChance3Value))
    //        {
    //            criticalChance3[i] = criticalChance3Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalChance4"].ToString(), out float criticalChance4Value))
    //        {
    //            criticalChance4[i] = criticalChance4Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalDamage1"].ToString(), out float criticalDamage1Value))
    //        {
    //            criticalDamage1[i] = criticalDamage1Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalDamage2"].ToString(), out float criticalDamage2Value))
    //        {
    //            criticalDamage2[i] = criticalDamage2Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalDamage3"].ToString(), out float criticalDamage3Value))
    //        {
    //            criticalDamage3[i] = criticalDamage3Value;
    //        }

    //        if (float.TryParse(data[i]["CriticalDamage4"].ToString(), out float criticalDamage4Value))
    //        {
    //            criticalDamage4[i] = criticalDamage4Value;
    //        }

    //        if (float.TryParse(data[i]["CoolTime1"].ToString(), out float coolTime1Value))
    //        {
    //            coolTIme1[i] = coolTime1Value;
    //        }

    //        if (float.TryParse(data[i]["CoolTime2"].ToString(), out float coolTime2Value))
    //        {
    //            coolTIme2[i] = coolTime2Value;
    //        }

    //        if (float.TryParse(data[i]["CoolTime3"].ToString(), out float coolTime3Value))
    //        {
    //            coolTIme3[i] = coolTime3Value;
    //        }

    //        if (float.TryParse(data[i]["CoolTime4"].ToString(), out float coolTime4Value))
    //        {
    //            coolTIme4[i] = coolTime4Value;
    //        }

    //        if (float.TryParse(data[i]["KnockBack1"].ToString(), out float knockBack1Value))
    //        {
    //            knockBack1[i] = knockBack1Value;
    //        }

    //        if (float.TryParse(data[i]["KnockBack2"].ToString(), out float knockBack2Value))
    //        {
    //            knockBack2[i] = knockBack2Value;
    //        }

    //        if (float.TryParse(data[i]["KnockBack3"].ToString(), out float knockBack3Value))
    //        {
    //            knockBack3[i] = knockBack3Value;
    //        }

    //        if (float.TryParse(data[i]["KnockBack4"].ToString(), out float knockBack4Value))
    //        {
    //            knockBack4[i] = knockBack4Value;
    //        }

    //        if (float.TryParse(data[i]["Range1"].ToString(), out float range1Value))
    //        {
    //            range1[i] = range1Value;
    //        }

    //        if (float.TryParse(data[i]["Range2"].ToString(), out float range2Value))
    //        {
    //            range2[i] = range2Value;
    //        }

    //        if (float.TryParse(data[i]["Range3"].ToString(), out float range3Value))
    //        {
    //            range3[i] = range3Value;
    //        }

    //        if (float.TryParse(data[i]["Range4"].ToString(), out float range4Value))
    //        {
    //            range4[i] = range4Value;
    //        }

    //        penetrate[i] = (int)data[i]["Penetrate"];

    //        if (float.TryParse(data[i]["PenetrateDamage"].ToString(), out float penetrateDamageValue))
    //        {
    //            penetrateDamage[i] = penetrateDamageValue;
    //        }

    //        type[i] = (int)data[i]["Type"];
    //        weaponType[i] = (string)data[i]["WeaponType"];
    // }
    //}
}
