using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatImporter : MonoBehaviour
{
    public static UpgradeStatImporter instance;

    public int[] upgradeNum;
    public string[] upgradeName;
    public string[] upgradeEffect;
    public int[] heart;
    public int[] lungs;
    public int[] teeth;
    public int[] triceps;
    public int[] forearms;
    public int[] shoulders;
    public int[] reflexes;
    public int[] fingers;
    public int[] skull;
    public int[] eyes;
    public int[] chest;
    public int[] back;
    public int[] legs;

    void Awake()
    {
        instance = this;
        List<Dictionary<string, object>> data = CSVReaderStat.Read("UpgradeStat");
        upgradeNum = new int[data.Count];
        upgradeName = new string[data.Count];
        upgradeEffect = new string[data.Count];
        heart = new int[4];
        lungs = new int[4];
        teeth = new int[4];
        triceps = new int[4];
        forearms = new int[4];
        shoulders = new int[4];
        reflexes = new int[4];
        fingers = new int[4];
        skull = new int[4];
        eyes = new int[4];
        chest = new int[4];
        back = new int[4];
        legs = new int[4];

        for (int i = 0; i < data.Count; i++)
        {
            upgradeNum[i] = (int)data[i]["UpgradeNum"];
            upgradeName[i] = (string)data[i]["UpgradeName"];
            upgradeEffect[i] = (string)data[i]["StatEffect"];
        }
        for (int i = 1; i < 5; i++)
        {
            heart[i - 1] = (int)data[0]["Tier" + i.ToString("F0")];
            lungs[i - 1] = (int)data[1]["Tier" + i.ToString("F0")];
            teeth[i - 1] = (int)data[2]["Tier" + i.ToString("F0")];
            triceps[i - 1] = (int)data[3]["Tier" + i.ToString("F0")];
            forearms[i - 1] = (int)data[4]["Tier" + i.ToString("F0")];
            shoulders[i - 1] = (int)data[5]["Tier" + i.ToString("F0")];
            reflexes[i - 1] = (int)data[6]["Tier" + i.ToString("F0")];
            fingers[i - 1] = (int)data[7]["Tier" + i.ToString("F0")];
            skull[i - 1] = (int)data[8]["Tier" + i.ToString("F0")];
            eyes[i - 1] = (int)data[9]["Tier" + i.ToString("F0")];
            chest[i - 1] = (int)data[10]["Tier" + i.ToString("F0")];
            back[i - 1] = (int)data[11]["Tier" + i.ToString("F0")];
            legs[i - 1] = (int)data[12]["Tier" + i.ToString("F0")];
        }
    }
}
