using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatImporter : MonoBehaviour
{
    public static UpgradeStatImporter instance;

    [HideInInspector] public int[] upgradeNum;
    [HideInInspector] public string[] upgradeName;
    [HideInInspector] public string[] upgradeEffect;
    [HideInInspector] public float[] heart;
    [HideInInspector] public float[] lungs;
    [HideInInspector] public float[] teeth;
    [HideInInspector] public float[] triceps;
    [HideInInspector] public float[] forearms;
    [HideInInspector] public float[] shoulders;
    [HideInInspector] public float[] reflexes;
    [HideInInspector] public float[] fingers;
    [HideInInspector] public float[] skull;
    [HideInInspector] public float[] eyes;
    [HideInInspector] public float[] chest;
    [HideInInspector] public float[] back;
    [HideInInspector] public float[] legs;

    void Awake()
    {
        instance = this;
        string filePath = "Assets/Resources/CSV.data/StatInfo/UpgradeStat.xlsx";
        List<Dictionary<int, object>> data = ExcelReader.ReadNumericColumns(filePath);
        upgradeNum = new int[data.Count];
        upgradeName = new string[data.Count];
        upgradeEffect = new string[data.Count];
        heart = new float[4];
        lungs = new float[4];
        teeth = new float[4];
        triceps = new float[4];
        forearms = new float[4];
        shoulders = new float[4];
        reflexes = new float[4];
        fingers = new float[4];
        skull = new float[4];
        eyes = new float[4];
        chest = new float[4];
        back = new float[4];
        legs = new float[4];

        for (int i = 0; i < data.Count; i++)
        {
            upgradeNum[i] = (int)data[i][0];
            upgradeName[i] = (string)data[i][1];
            upgradeEffect[i] = (string)data[i][2];
        }
        for (int i = 0; i < 4; i++)
        {
            if (float.TryParse(data[0][i + 3].ToString(), out float heartValue))
            {
                heart[i] = heartValue;
            }
            if (float.TryParse(data[1][i + 3].ToString(), out float lungsValue))
            {
                lungs[i] = lungsValue;
            }
            if (float.TryParse(data[2][i + 3].ToString(), out float teethtValue))
            {
                teeth[i] = teethtValue;
            }
            if (float.TryParse(data[3][i + 3].ToString(), out float tricepsValue))
            {
                triceps[i] = tricepsValue;
            }
            if (float.TryParse(data[4][i + 3].ToString(), out float forearmsValue))
            {
                forearms[i] = forearmsValue;
            }
            if (float.TryParse(data[5][i + 3].ToString(), out float shouldersValue))
            {
                shoulders[i] = shouldersValue;
            }
            if (float.TryParse(data[6][i + 3].ToString(), out float reflexesValue))
            {
                reflexes[i] = reflexesValue;
            }
            if (float.TryParse(data[7][i + 3].ToString(), out float fingersValue))
            {
                fingers[i] = fingersValue;
            }
            if (float.TryParse(data[8][i + 3].ToString(), out float skullValue))
            {
                skull[i] = skullValue;
            }
            if (float.TryParse(data[9][i + 3].ToString(), out float eyesValue))
            {
                eyes[i] = eyesValue;
            }
            if (float.TryParse(data[10][i + 3].ToString(), out float chestValue))
            {
                chest[i] = chestValue;
            }
            if (float.TryParse(data[11][i + 3].ToString(), out float backValue))
            {
                back[i] = backValue;
            }
            if (float.TryParse(data[12][i + 3].ToString(), out float legsValue))
            {
                legs[i] = legsValue;
            }
        }
    }
    
}
