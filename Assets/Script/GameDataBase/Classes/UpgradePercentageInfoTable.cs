using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Only1Games.GDBA;
public class UpgradePercentageInfoTable : GameDataTable<UpgradePercentageInfoTable.Data>
{
    [System.Serializable]
    public class Data : GameData
    {
        public string tier;
        public int minLevel;
        public float baseChance;
        public float chancePerLevel;
        public float maxChance;
    }
}
