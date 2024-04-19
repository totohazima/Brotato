using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ShopBasePriceImporter : MonoBehaviour
{
    public static ShopBasePriceImporter instance;

    public string[] itemCode;
    public float[] itemBasePrice;

    public string[] weaponCode;
    public float[] weaponBasePrice;
    void Awake()
    {
        instance = this;
        string filePath;
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, "ItemBasePrice.xlsx");
#elif UNITY_IOS && !UNITY_EDITOR
            filePath = Path.Combine(Application.persistentDataPath, "ItemBasePrice.xlsx");
#else
        filePath = "Assets/Resources/CSV.data/StatInfo/ItemBasePrice.xlsx";
#endif
        List<Dictionary<int, object>> data = CSVReaderStat.ReadNumericColumns("ItemBasePrice");

        itemCode = new string[data.Count];
        itemBasePrice = new float[data.Count];

        for (int i = 0; i < data.Count; i++)
        {
            itemCode[i] = (string)data[i][0];
            if (float.TryParse(data[i][1].ToString(), out float itemBasePriceValue))
            {
                itemBasePrice[i] = itemBasePriceValue;
            }
        }


        string filePath1;
#if UNITY_ANDROID && !UNITY_EDITOR
            filePath1 = Path.Combine(Application.streamingAssetsPath, "CSV.data/StatInfo/WeaponBasePrice.xlsx");
#elif UNITY_IOS && !UNITY_EDITOR
            filePath1 = Path.Combine(Application.streamingAssetsPath, "CSV.data/StatInfo/WeaponBasePrice.xlsx");
#else
        filePath1 = "Assets/Resources/CSV.data/StatInfo/WeaponBasePrice.xlsx";
#endif
        List<Dictionary<int, object>> data1 = CSVReaderStat.ReadNumericColumns("WeaponBasePrice");

        weaponCode = new string[data1.Count];
        weaponBasePrice = new float[data1.Count];

        for (int i = 0; i < data1.Count; i++)
        {
            weaponCode[i] = (string)data1[i][0];
            if (float.TryParse(data1[i][1].ToString(), out float itemBasePriceValue))
            {
                weaponBasePrice[i] = itemBasePriceValue;
            }
        }
    }
}
